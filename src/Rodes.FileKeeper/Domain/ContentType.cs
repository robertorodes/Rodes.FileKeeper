using Rodes.FileKeeper.Domain.Assertions;

namespace Rodes.FileKeeper.Domain
{
    public class ContentType : ValueObject<ContentType>
    {
        #region Attributes

        private string type;

        #endregion

        #region Constructors

        private ContentType()
        { }

        public ContentType(string type)
        {
            this.Type = type;
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return this.Type;
        }

        #endregion

        #region Properties

        public string Type
        {
            get
            {
                return this.type;
            }
            private set
            {
                AssertionHelper.AssertNotNull(value, "Type", Resources.Messages.Error_ContentType_Type_IsNullOrEmpty);
                AssertionHelper.AssertNotWhiteSpace(value, "Type", Resources.Messages.Error_ContentType_Type_IsNullOrEmpty);

                //TODO: Check that the content type is actually supported
                this.type = value;
            }
        }

        #endregion
    }
}
