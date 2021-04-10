using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletWPF
{
    public  class AutentificationService
    {
        public User Authenticate(AuthenticationUser User)
        {
            if (String.IsNullOrWhiteSpace(User.Login) || String.IsNullOrWhiteSpace(User.Password))
                throw new ArgumentException("Empty field");
        }

        public AutentificationService(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
