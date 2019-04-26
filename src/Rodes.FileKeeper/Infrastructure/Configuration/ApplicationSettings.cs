using Rodes.FileKeeper.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodes.FileKeeper.Infrastructure.Configuration
{
    public class ApplicationSettings
    {
        private const string ConnectionStringDefaultLoadingErrorMessage = "The specified connection string ('{0}') does not exist in the configuration file. Please, make sure that it is added to the configuration file or that you are using the proper connection string name before loading this setting.";
        private const string SettingLoadingDefaultErrorMessage = "The specified application setting ('{0}') does not exist in the configuration file. Please, make sure that it is added to the configuration file or that you are using the proper application setting name before loading it.";

        public class Storage
        {
            public const string FileStoreConnectionStringName = "Rodes.FileKeeper.Storage.FileStore";
            public const string DataStoreConnectionStringName = "Rodes.FileKeeper.Storage.DataStore";

            public static string FileStoreConnectionString
            {
                get
                {
                    if (ConfigurationManager.ConnectionStrings[FileStoreConnectionStringName] == null)
                    {
                        throw new FileStorageConfigurationException(string.Format(ConnectionStringDefaultLoadingErrorMessage,
                            FileStoreConnectionStringName));
                    }

                    return ConfigurationManager.ConnectionStrings[FileStoreConnectionStringName].ConnectionString;
                }
            }

            public static string DataStoreConnectionString
            {
                get
                {
                    if (ConfigurationManager.ConnectionStrings[DataStoreConnectionStringName] == null)
                    {
                        throw new FileStorageConfigurationException(string.Format(ConnectionStringDefaultLoadingErrorMessage,
                            DataStoreConnectionStringName));
                    }

                    return ConfigurationManager.ConnectionStrings[DataStoreConnectionStringName].ConnectionString;
                }
            }

            public class AzureBlobs
            {
                private const string PublicContainerNameSettingName = "Rodes.FileKeeper.Storage.AzureBlobs.PublicContainerName";
                private const string PrivateContainerNameSettingName = "Rodes.FileKeeper.Storage.AzureBlobs.PrivateContainerName";
                private const string DefaultPublicContainerName = "publicfiles";
                private const string DefaultPrivateContainerName = "privatefiles";

                public static string PublicContainerName
                {
                    get
                    {
                        if (ConfigurationManager.AppSettings[PublicContainerNameSettingName] == null)
                        {
                            return DefaultPublicContainerName;
                        }
                        else
                        {
                            return ConfigurationManager.AppSettings[PublicContainerNameSettingName];
                        }
                    }
                }

                public static string PrivateContainerName
                {
                    get
                    {
                        if (ConfigurationManager.AppSettings[PrivateContainerNameSettingName] == null)
                        {
                            return DefaultPrivateContainerName;
                        }
                        else
                        {
                            return ConfigurationManager.AppSettings[PrivateContainerNameSettingName];
                        }
                    }
                }
            }
        }

        public class Routing
        {
            private const string LocalFileAccessBaseUriSettingName = "Rodes.FileKeeper.Routing.LocalFileAccessBaseUri";

            public static string LocalFilesAccessBaseUri
            {
                get
                {
                    if (ConfigurationManager.AppSettings[LocalFileAccessBaseUriSettingName] == null)
                    {
                        throw new FileStorageConfigurationException(string.Format(SettingLoadingDefaultErrorMessage,
                            LocalFileAccessBaseUriSettingName));
                    }

                    return ConfigurationManager.AppSettings[LocalFileAccessBaseUriSettingName];
                }
            }
        }
    }
}
