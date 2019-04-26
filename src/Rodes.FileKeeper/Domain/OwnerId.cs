using Rodes.FileKeeper.Domain.Assertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.Domain
{
    public class OwnerId : ValueObject<OwnerId>
    {
        #region Attributes

        private OwnerType ownerType;
        private string userId;
        private string businessId;

        #endregion

        #region Constructors

        private OwnerId() { }

        public OwnerId(string businessId, string userId)
        {
            this.UserId = userId;

            if (businessId != null && !string.IsNullOrWhiteSpace(businessId))
            {
                this.OwnerType = Domain.OwnerType.Business;
                this.BusinessId = businessId;
            }
            else
            {
                this.OwnerType = Domain.OwnerType.Individual;
                this.BusinessId = null;
            }
        }

        #endregion

        #region Constructors

        #endregion

        #region Properties

        public OwnerType OwnerType
        {
            get
            {
                return this.ownerType;
            }
            private set
            {
                AssertionHelper.AssertIsInEnum<OwnerType>(value, "OwnerType", Resources.Messages.Error_OwnerId_OwnerType_IsNotValid);
                this.ownerType = value;
            }
        }

        public string UserId
        {
            get
            {
                return this.userId;
            }
            private set
            {
                AssertionHelper.AssertNotNull(value, "UserId", Resources.Messages.Error_OwnerId_UserId_IsNullOrEmpty);
                AssertionHelper.AssertNotWhiteSpace(value, "UserId", Resources.Messages.Error_OwnerId_UserId_IsNullOrEmpty);
                this.userId = value;
            }
        }

        public string BusinessId
        {
            get
            {
                return this.businessId;
            }
            private set
            {
                this.businessId = value;
            }
        }

        #endregion
    }
}
