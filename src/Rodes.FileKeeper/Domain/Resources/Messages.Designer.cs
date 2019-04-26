﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Rodes.FileKeeper.Domain.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Messages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Messages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Rodes.FileKeeper.Domain.Resources.Messages", typeof(Messages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Content type cannot be null or empty. Please, specify a valid MIME content type..
        /// </summary>
        internal static string Error_ContentType_Type_IsNullOrEmpty {
            get {
                return ResourceManager.GetString("Error_ContentType_Type_IsNullOrEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Id cannot be empty. Please, specify a valid identifier..
        /// </summary>
        internal static string Error_Entity_Id_IsEmpty {
            get {
                return ResourceManager.GetString("Error_Entity_Id_IsEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The content length of the file cannot be less than one byte. Please, specify a valid file size..
        /// </summary>
        internal static string Error_FileDescription_ContentLength_IsLessThanOne {
            get {
                return ResourceManager.GetString("Error_FileDescription_ContentLength_IsLessThanOne", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The content type cannot be null. Please, specify a valid MIME content type..
        /// </summary>
        internal static string Error_FileDescription_ContentType_IsNull {
            get {
                return ResourceManager.GetString("Error_FileDescription_ContentType_IsNull", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The file name cannot be null or empty. Please, specify a valid file name..
        /// </summary>
        internal static string Error_FileDescription_FileName_IsNullOrEmpty {
            get {
                return ResourceManager.GetString("Error_FileDescription_FileName_IsNullOrEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The specified owner type is not valid. Please, specify a valid owner type..
        /// </summary>
        internal static string Error_OwnerId_OwnerType_IsNotValid {
            get {
                return ResourceManager.GetString("Error_OwnerId_OwnerType_IsNotValid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The user id specified cannot be null or empty. Please, specify a valid user identifier..
        /// </summary>
        internal static string Error_OwnerId_UserId_IsNullOrEmpty {
            get {
                return ResourceManager.GetString("Error_OwnerId_UserId_IsNullOrEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There are missing files in your session. Either the upload process failed or they were not uploaded at all. Please, make sure to upload all files before ending the upload session or retry starting a new session and uploading your files again..
        /// </summary>
        internal static string Error_UploadSession_EndMethod_MissingFiles {
            get {
                return ResourceManager.GetString("Error_UploadSession_EndMethod_MissingFiles", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The session is already canceled..
        /// </summary>
        internal static string Error_UploadSession_EndMethod_SessionIsCanceled {
            get {
                return ResourceManager.GetString("Error_UploadSession_EndMethod_SessionIsCanceled", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An upload session cannot be initiated without specifying files to be uploaded. Please, specify at least one file description for the upload session..
        /// </summary>
        internal static string Error_UploadSession_FileDescriptions_IsNullOrEmpty {
            get {
                return ResourceManager.GetString("Error_UploadSession_FileDescriptions_IsNullOrEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The upload session has not been started yet..
        /// </summary>
        internal static string Error_UploadSession_GetFileUploadDescriptionMethod_SessioIsNotStarted {
            get {
                return ResourceManager.GetString("Error_UploadSession_GetFileUploadDescriptionMethod_SessioIsNotStarted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The session is not currently opened (it was likely successfully ended or canceled). Please, start a new session and upload the files again..
        /// </summary>
        internal static string Error_UploadSession_MarkFilesAsUploadedMethod_SessionIsNotStarted {
            get {
                return ResourceManager.GetString("Error_UploadSession_MarkFilesAsUploadedMethod_SessionIsNotStarted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Some of the specified files are not registered in upload session with id &apos;{0}&apos;. Please, make sure that the file has been properly uploaded and that you are not mixing files from different upload sessions for this operation. See the inner exception for details..
        /// </summary>
        internal static string Error_UploadSession_MarkFilesAsUsedMethod_FileIsNotRegistered {
            get {
                return ResourceManager.GetString("Error_UploadSession_MarkFilesAsUsedMethod_FileIsNotRegistered", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The upload process is not completed. Please, make sure that all files have been successfully uploaded first and the session has been ended..
        /// </summary>
        internal static string Error_UploadSession_MarkFilesAsUsedMethod_UploadIsNotCompleted {
            get {
                return ResourceManager.GetString("Error_UploadSession_MarkFilesAsUsedMethod_UploadIsNotCompleted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The owner identifier cannot be null. Please specify a valid owner id..
        /// </summary>
        internal static string Error_UploadSession_OwnerId_IsNull {
            get {
                return ResourceManager.GetString("Error_UploadSession_OwnerId_IsNull", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The specified session state is not valid. Please, specify a valid session state..
        /// </summary>
        internal static string Error_UploadSession_State_IsNotValid {
            get {
                return ResourceManager.GetString("Error_UploadSession_State_IsNotValid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The upload session is already canceled..
        /// </summary>
        internal static string Error_UploadSessionCancelledException_DefaultMessage {
            get {
                return ResourceManager.GetString("Error_UploadSessionCancelledException_DefaultMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The specified file (&apos;{0}&apos;) does not exist. Please, specify a valid file identifier..
        /// </summary>
        internal static string Error_UploadSessionFileNotRegisteredException_DefaultDetailedMessage {
            get {
                return ResourceManager.GetString("Error_UploadSessionFileNotRegisteredException_DefaultDetailedMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The specified file does not exist. Please, specify a valid file identifier..
        /// </summary>
        internal static string Error_UploadSessionFileNotRegisteredException_DefaultMessage {
            get {
                return ResourceManager.GetString("Error_UploadSessionFileNotRegisteredException_DefaultMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The upload process is not completed..
        /// </summary>
        internal static string Error_UploadSessionNotCompletedException_DefaultMessage {
            get {
                return ResourceManager.GetString("Error_UploadSessionNotCompletedException_DefaultMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No upload session has been started. Please, make sure that the upload session has been previously started and is not finished..
        /// </summary>
        internal static string Error_UploadSessionNotStartedException_DefaultMessage {
            get {
                return ResourceManager.GetString("Error_UploadSessionNotStartedException_DefaultMessage", resourceCulture);
            }
        }
    }
}