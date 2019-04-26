using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.Domain
{
    public class ResourceUri : ValueObject<ResourceUri>
    {
        #region Attributes

        private string uri;

        #endregion

        #region Constructors

        private ResourceUri()
        {
            this.Uri = null;
        }

        public ResourceUri(string uri)
        {
            this.Uri = uri;
        }

        #endregion

        #region Properties

        public static ResourceUri Empty
        {
            get
            {
                return new ResourceUri();
            }
        }

        public string Uri
        {
            get
            {
                return this.uri;
            }
            private set
            {
                //TODO: Add validation code
                this.uri = value;
            }
        }

        #endregion
    }
}
