﻿using System.Web.Security;
using Coevery.ContentManagement;
using Coevery.Security;

namespace Coevery.Users.Models {
    public sealed class UserPart : ContentPart<UserPartRecord>, IUser {
        public const string EmailPattern = 
            @"^(?![\.@])(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
            + @"@([a-z0-9][\w-]*\.)+[a-z]{2,}$";

        public string UserName {
            get { return Retrieve(x => x.UserName); }
            set { Store(x => x.UserName, value); }
        }

        public string EmailChallengeToken {
            get { return Retrieve(x => x.EmailChallengeToken); }
            set { Store(x => x.EmailChallengeToken, value); }
        }

        public string HashAlgorithm {
            get { return Retrieve(x => x.HashAlgorithm); }
            set { Store(x => x.HashAlgorithm, value); }
        }

        public string Password {
            get { return Retrieve(x => x.Password); }
            set { Store(x => x.Password, value); }
        }

        public MembershipPasswordFormat PasswordFormat {
            get { return Retrieve(x => x.PasswordFormat); }
            set { Store(x => x.PasswordFormat, value); }
        }

        public string PasswordSalt {
            get { return Retrieve(x => x.PasswordSalt); }
            set { Store(x => x.PasswordSalt, value); }
        }

        public string Email {
            get { return Retrieve(x => x.Email); }
            set { Store(x => x.Email, value); }
        }

        public string NormalizedUserName {
            get { return Retrieve(x => x.NormalizedUserName); }
            set { Store(x => x.NormalizedUserName, value); }
        }

        public UserStatus RegistrationStatus {
            get { return Retrieve(x => x.RegistrationStatus); }
            set { Store(x => x.RegistrationStatus, value); }
        }

        public UserStatus EmailStatus {
            get { return Retrieve(x => x.EmailStatus); }
            set { Store(x => x.EmailStatus, value); }
        }
    }
}
