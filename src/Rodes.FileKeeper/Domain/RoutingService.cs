using Rodes.FileKeeper.Infrastructure.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.Domain
{
    public static class RoutingService
    {
        #region Methods

        public static ResourceUri BuildUriForFile(FileUploadDescription fileDescription, ResourceUri sourceStoreFileUri, bool isFilePublic)
        {
            // if filesourcestoreuri==null then local uri
            // else if access is private then local uri
            // else if access is public and filesourcestoreuri!=null then filesourcestoreuri
            ResourceUri route = null;
            string localUri = string.Format("{0}{1}", ApplicationSettings.Routing.LocalFilesAccessBaseUri, fileDescription.Id);
            if (sourceStoreFileUri == null)
            {
                route = new ResourceUri(localUri);
            }
            else
            {
                if (!isFilePublic)
                {
                    route = new ResourceUri(localUri);
                }
                else
                {
                    route = sourceStoreFileUri;
                }
            }

            return route;
        }

        #endregion
    }
}
