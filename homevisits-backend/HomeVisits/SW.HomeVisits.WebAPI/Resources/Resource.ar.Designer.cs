﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SW.HomeVisits.WebAPI.Resources {
    using System;
    using System.Reflection;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resource_ar {
        
        private static System.Resources.ResourceManager resourceMan;
        
        private static System.Globalization.CultureInfo resourceCulture;
        
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resource_ar() {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public static System.Resources.ResourceManager ResourceManager {
            get {
                if (object.Equals(null, resourceMan)) {
                    System.Resources.ResourceManager temp = new System.Resources.ResourceManager("SW.HomeVisits.WebAPI.Resources.Resource.ar", typeof(Resource_ar).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public static System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        public static string ChemistNoScheduleFound {
            get {
                return ResourceManager.GetString("ChemistNoScheduleFound", resourceCulture);
            }
        }
        
        public static string LostTimeSavedSuccess {
            get {
                return ResourceManager.GetString("LostTimeSavedSuccess", resourceCulture);
            }
        }
        
        public static string PatientsNotFound {
            get {
                return ResourceManager.GetString("PatientsNotFound", resourceCulture);
            }
        }
        
        public static string PatientVisitsNotFound {
            get {
                return ResourceManager.GetString("PatientVisitsNotFound", resourceCulture);
            }
        }
        
        public static string VisitCompletedSuccessfully {
            get {
                return ResourceManager.GetString("VisitCompletedSuccessfully", resourceCulture);
            }
        }
        
        public static string VisitDetailsNotFound {
            get {
                return ResourceManager.GetString("VisitDetailsNotFound", resourceCulture);
            }
        }
        
        public static string VisitIsCancelled {
            get {
                return ResourceManager.GetString("VisitIsCancelled", resourceCulture);
            }
        }
        
        public static string VisitNotificationNotFound {
            get {
                return ResourceManager.GetString("VisitNotificationNotFound", resourceCulture);
            }
        }
    }
}
