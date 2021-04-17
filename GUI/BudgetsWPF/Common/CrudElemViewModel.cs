#nullable enable
using System;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;

namespace AV.ProgrammingWithCSharp.Budgets.GUI.WPF.Common
{
    public enum EntityState
    {
        Added, Unchanged, Modified, Pending
    }
    public abstract class CrudElemViewModel<T> : BindableBase where T:new() 
    {
       public T Item { get; private set; }
       private EntityState _itemState;
       public EntityState GetItemState() => ItemState;
       protected EntityState ItemState
       {
           get => _itemState;
           set
           {
               _itemState = value;
               RaisePropertyChanged();
           }
       }

       protected CrudElemViewModel(T? item, Action onItemDelete)
       {
           if (item is null)
           {
               Item = new T();
               ItemState = EntityState.Added;
           }
           else
           {
               Item = item;
               ItemState = EntityState.Unchanged;
           }

           SaveCommand = new DelegateCommand(async () =>
           {
               bool toAdd = ItemState == EntityState.Added;
               ItemState = EntityState.Pending;
               if (toAdd)
                   await Add();
               else 
                   await Save();
               Item = await Get();
               ItemState = EntityState.Unchanged;
           }, () => ItemState is EntityState.Modified or EntityState.Added);
           DeleteCommand = new DelegateCommand(async () =>
           {
               if (ItemState == EntityState.Added)
               {
                   onItemDelete();
                   return;
               }
               ItemState = EntityState.Pending;
               await Delete();
               onItemDelete();
           }, () => ItemState is not EntityState.Pending);
           PropertyChanged += (sender, args) =>
           {
               SaveCommand.RaiseCanExecuteChanged();
               DeleteCommand.RaiseCanExecuteChanged();
           };
       }

       protected abstract Task Save();
       protected abstract Task Delete();
       protected abstract Task Add();
       protected abstract Task<T> Get();

       protected void Setter(Action propAction)
       {
           if(ItemState == EntityState.Pending)
               return;
           propAction();
           if(ItemState != EntityState.Added)
               ItemState = EntityState.Modified;
           RaisePropertyChanged();
       }
       
        public DelegateCommand SaveCommand { get; }
        public DelegateCommand DeleteCommand { get; }
    }
}