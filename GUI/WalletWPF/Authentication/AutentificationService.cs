using System;

namespace WalletWPF.Authentication
{
    public  class AutentificationService
    {
        public User Authenticate(AuthenticationUser User)
        {
            if (String.IsNullOrWhiteSpace(User.Login) || String.IsNullOrWhiteSpace(User.Password))
                throw new ArgumentException("Empty field");
            //To do logic(method for user login, validation and retrieve user from storage)
            return new User(Guid.NewGuid(), "Andrii", "Makarets", "---", "ShadowDragon");
        }

    }
}
