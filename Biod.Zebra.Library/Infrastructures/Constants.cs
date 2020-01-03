using System;

namespace Biod.Zebra.Library.Infrastructures
{
    public static class Constants
    {
        public static string LITMUS_DATA_DELIMITER = "|";

        public static class IdentityTokenPurpose
        {
            public static string EMAIL_CONFIRMATION = "Confirmation"; // Tokens created for Email Confirmation by the User Manager
            public static string PASSWORD_RESET = "ResetPassword"; // Tokens created for Password Reset by the User Manager
        }

        public static class LoginHeader
        {
            public static string TOKEN_AUTHORIZATION = "Authorization";
            public static string USERNAME = "username";
            public static string PASSWORD = "password";
            public static string FIREBASE_DEVICE_ID = "fcmdeviceid";
        }

        public static class EmailTypes
        {
            public const int EMAIL_CONFIRMATION = 1;
            public const int WELCOME_EMAIL = 2;
            public const int RESET_PASSWORD_EMAIL = 3;
            public const int EVENT_EMAIL = 4;
            public const int WEEKLY_BRIEF_EMAIL = 5;
            public const int PROXIMAL_EMAIL = 6;
        }

        public static class ProximalEmail
        {
            public static int RECENT_THRESHOLD_IN_DAYS = 7;  // Updated reported case count is considered recent if the date is within the past RECENT_THRESHOLD days
        }

        public static class PreventionTypes
        {
            public static string BEHAVIOURAL = "Behavioural only";
        }

        public static class LocationType
        {
            public const int COUNTRY = 6;
            public const int PROVINCE = 4;
            public const int CITY = 2;
        }
        public static class LocationTypeDescription
        {
            public const string SUMMARY = "-";
            public const string COUNTRY = "Country";
            public const string PROVINCE = "Province/State";
            public const string CITY = "City/Township";
        }

        public static class LocationHistoryDataType
        {
            public static int PROXIMAL_DATA = 1;
        }

        public static class Geoname
        {
            public const int ID_SUMMARY = -1;
            public const int UNKNOWN_COUNTRY = -1;
        }

        public static class RelevanceTypes
        {
            public const int ALWAYS_NOTIFY = 1;
            public const int RISK_ONLY = 2;
            public const int NEVER_NOTIFY = 3;
        }

        public enum NotificationTypes { EMAIL, PUSH };

        public static class OrderByFieldTypes
        {
            public const int LAST_UPDATED = 1;
            public const int EVENT_START_DATE = 2;
            [Obsolete("This order by option is not available to the user")]
            public const int RISK_LIKELIHOOD = 3;
            public const int RISK_OF_EXPORTATION = 4;
            public const int CASE_COUNT = 5;
            public const int DEATH_COUNT = 6;
            public const int RISK_OF_IMPORTATION = 7;
        }

        public static class GroupByFieldTypes
        {
            public const int NONE = 1;
            [Obsolete("This group by option is not available to the user")]
            public const int LOCAL_VS_GLOBAL = 2;
            [Obsolete("This group by option is not available to the user")]
            public const int DISEASE_NAME = 3;
            public const int TRANSMISSION_MODE = 4;
            [Obsolete("This group by option is not available to the user")]
            public const int LOCAL_TRANSMISSION_POSSIBILITY = 5;
            public const int BIOSECURITY_RISK = 6;
            public const int PREVENTION_MEASURE = 7;
            public const int DISEASE_RISK = 8;
        }

        public static class RiskLevel
        {
            public const int UNKNOWN = -1;
            public const int NEGLIGIBLE = 0;
            public const int LOW = 1;
            public const int MEDIUM = 2;
            public const int HIGH = 3;
        }


        public static class ExternalIdentifiers
        {
            public static string GOOGLE_ANALYTICS = "Google Analytics";
            public static string FIREBASE_FCM = "Firebase Cloud Messaging";
        }

        public static class GoogleAnalytics
        {
            public static class Action
            {
                public static string CHANGE_DISEASE_MATRIX_GROUPING = "Change Grouping of Disease Matrix";
                public static string CHANGE_GROUPING = "Change Grouping";
                public static string CHANGE_SORTING = "Change Sorting";
                public static string CLICK_ABOUT = "Click About";
                public static string CLICK_ACCOUNT_DETAILS = "Click Account Details";
                public static string CLICK_ADD_FILTERS = "Click Add Filters";
                public static string CLICK_APPLY_FILTERS = "Click Apply Filters button";
                public static string CLICK_CHANGE_PASSWORD = "Click Change Password";
                public static string CLICK_CLEAR_ALL_FILTERS = "Click Clear All Filters";
                public static string CLICK_CONTACT_SALES = "Click Contact Sales";
                public static string CLICK_CONTACT_US = "Click Contact Us";
                public static string CLICK_CURRENT_LOCATION_ICON = "Click Current Location icon";
                public static string CLICK_CUSTOM_SETTINGS = "Click Custom Settings";
                public static string CLICK_CUSTOMIZE_DISEASES = "Click Customize My Diseases button";
                public static string CLICK_EDIT_CUSTOMIZATIONS = "Click Edit my customizations";
                public static string CLICK_EDIT_CUSTOMIZATIONS_FROM_ROLE_DESCRIPTION = "Click Edit customized diseases from within role description";
                public static string CLICK_EVENT_TOOLTIP = "Click on event within tooltip";
                public static string CLICK_FINISH_ONBOARDING = "Click Finish on onboarding screen";
                public static string CLICK_FORGOT_PASSWORD = "Click Forgot Password";
                public static string CLICK_HIDE_CUSTOMIZED_DISEASES = "Click Hide Customized Diseases button";
                public static string CLICK_LOG_OUT = "Click Log Out";
                public static string CLICK_MAP = "Click on Map";
                public static string CLICK_MAP_PIN = "Click on map pin";
                public static string CLICK_MODIFY_FILTERS = "Click Modify Filters";
                public static string CLICK_NAVIGATION_BAR_LINK = "Click link in Navigation bar";
                public static string CLICK_NOTIFICATIONS = "Click Notifications";
                public static string CLICK_NEXT_ONBOARDING = "Click Next on onboarding screen";
                public static string CLICK_OUTBREAK_POTENTIAL_COLLAPSE = "Click to collapse outbreak potential";
                public static string CLICK_OUTBREAK_POTENTIAL_EXPAND = "Click to expand outbreak potential";
                public static string CLICK_PREVIOUS_ONBOARDING = "Click Previous on onboarding screen";
                public static string CLICK_PRIVACY_POLICY = "Click Privacy Policy";
                public static string CLICK_READ_MORE = "Click Read More";
                public static string CLICK_REFERENCE_LINK = "Click on Reference link";
                public static string CLICK_RESET_FILTER_AOI = "Click Reset to Default for AOI";
                public static string CLICK_RESET_TO_ROLE_PRESET = "Click Reset to presets";
                public static string CLICK_SEE_CUSTOM_EVENTS = "Click See my custom events";
                public static string CLICK_SEE_ALL_EVENTS = "Click See all events";
                public static string CLICK_SIGN_IN = "Click Sign In button";
                public static string CLICK_SIGN_IN_REMEMBER_ME = "Click on \"Remember me\" checkbox on Sign In page";
                public static string CLICK_SIGN_UP = "Click Sign Up button";
                public static string CLICK_TERMS_OF_SERVICE = "Click Terms of Service";
                public static string CLICK_UPDATE_CUSTOM_SETTINGS = "Click Update Custom Settings";
                public static string CLICK_ZOOM_IN = "Click on Zoom In";
                public static string CLICK_ZOOM_OUT = "Click on Zoom Out";
                public static string CLOSE_COUNTRY_TOOLTIP = "Close country tooltip";
                public static string CLOSE_EVENT_LIST = "Close Event List";
                public static string CLOSE_FILTERS_PANEL_X_ICON = "Close filters panel with X";
                public static string COLLAPSE_ADDITIONAL_EVENTS_EVENT_LIST = "Collapse additional events with risk to user locations";
                public static string COLLAPSE_EVENT_DETAILS_SECTION = "Collapse Event Details section";
                public static string COLLAPSE_DISEASE_GROUP_EVENT_LIST = "Collapse disease card in event list";
                public static string COLLAPSE_DISEASE_MATRIX_GROUP_BY_SECTION = "Collapse Group by section in Disease Matrix";
                public static string EXPAND_ADDITIONAL_EVENTS_EVENT_LIST = "Expand additional events with risk to user locations";
                public static string EXPAND_DISEASE_GROUP_EVENT_LIST = "Expand disease card in event list";
                public static string EXPAND_DISEASE_MATRIX_GROUP_BY_SECTION = "Expand Group by section in Disease Matrix";
                public static string NAVIGATE_FROM_REGISTRATION_TO_SIGN_IN = "Navigate to Sign In page from Registration page";
                public static string NAVIGATE_FROM_SIGN_IN_TO_REGISTRATION = "Navigate to Registration page from Sign In page";
                public static string OPEN_EVENT_DETAILS = "Open Event Details";
                public static string OPEN_EVENT_LIST = "Open Event List";
                public static string PAN_MAP = "Pan on Map";
                public static string RETURN_TO_EVENT_LIST_TOOLTIP = "Go back to event list within tooltip";
                public static string UPDATE_AOI = "Update Areas of Interest";
                public static string UPDATE_FILTERS_FROM_EVENT_LIST = "Update Filters from collapsible section of Event List";
                public static string UPDATE_NOTIFICATION_PREFERENCES = "Update Notification Preferences";
            }

            public static class Category
            {
                public static string AUTHENTICATION = "Authentication";
                public static string CONTACT = "Contact";
                public static string EMAIL = "Email";
                public static string EVENT_DETAILS = "Event Details";
                public static string EVENT_LIST = "Event List";
                public static string EVENTS = "Events";
                public static string FILTERS = "Filters";
                public static string MAP = "Map";
                public static string MAP_TOOLTIP = "Map Tooltip";
                public static string NAVIGATION = "Navigation";
                public static string PRE_AUTH = "Pre-Auth";
                public static string SETTINGS = "Settings";
                public static string ONBOARDING = "Onboarding";
            }

            public static class UrlTracking
            {
                public static string UTM_CAMPAIGN_CONFIRMATION = "confirmation";
                public static string UTM_CAMPAIGN_EVENT_ALERT = "alert_new";
                public static string UTM_CAMPAIGN_PROXIMAL = "local_activity";
                public static string UTM_CAMPAIGN_RESET_PASSWORD = "reset_password";
                public static string UTM_CAMPAIGN_WEEKLY_BRIEF = "weekly_brief";
                public static string UTM_CAMPAIGN_WELCOME = "welcome";
                public static string UTM_MEDIUM_EMAIL = "email";
                public static string UTM_SOURCE_EMAIL = "insights_email";
            }
        }
    }
}
