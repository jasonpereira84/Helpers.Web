using System;

namespace JasonPereira84.Helpers
{
    using Microsoft.AspNetCore.Http;

    using JasonPereira84.Helpers.Extensions;

    public abstract class _NavigationItem
    {
        public String Text { get; protected set; }

        public String Class { get; protected set; }

        public Boolean IsActive { get; protected set; }

        protected _NavigationItem(String text, String @class, Boolean isActive)
        {
            Text = text;
            Class = @class;
            IsActive = isActive;
        }
    }

    public class NavigationItem : _NavigationItem
    {
        public NavigationItem(String text, PathString href, String @class = default(String), Boolean isActive = default(Boolean))
            : base(text, @class.Sanitize(), isActive)
            => Href = href;

        public PathString Href { get; protected set; }

        public NavigationItem ActivateIfMatches(String @string)
        {
            IsActive = Href.Value.IsNotNullOrEmptyOrWhiteSpace() && Href.Value.Matches(@string);
            return this;
        }

        public NavigationItem ActivateIfMatches(PathString pathString)
        {
            if (pathString.Value.IsNullOrEmptyOrWhiteSpace())
                return this;

            return ActivateIfMatches(pathString.Value);
        }

        public static NavigationItem From(String text, PathString href, String @class = default(String), Boolean isActive = default(Boolean))
            => new NavigationItem(text, href, @class, isActive);
    }

    public class MvcNavigationItem : _NavigationItem
    {
        public MvcNavigationItem(String text, String controller, String action = "Index", String @class = default(String), Boolean isActive = default(Boolean))
            : base(text, @class.Sanitize(), isActive)
        {
            Controller = controller;
            Action = action;
        }

        public String Controller { get; protected set; }

        public String Action { get; protected set; }

        public static MvcNavigationItem From(String text, String controller, String action = "Index", String @class = default(String), Boolean isActive = default(Boolean))
            => new MvcNavigationItem(text, controller, action, @class, isActive);

        public static MvcNavigationItem From(String text, String controller, String action, ControllerActionRequestInformation requestInformation, String @class = default(String))
        {
            var retVal = new MvcNavigationItem(text, controller, action, @class);
            retVal.IsActive = requestInformation.Controller.IsNotNullOrEmptyOrWhiteSpace() && requestInformation.Action.IsNotNullOrEmptyOrWhiteSpace()
                && retVal.Controller.IsNotNullOrEmptyOrWhiteSpace() && retVal.Controller.Matches(requestInformation.Controller)
                && retVal.Action.IsNotNullOrEmptyOrWhiteSpace() && retVal.Action.Matches(requestInformation.Action);
            if (retVal.IsActive)
                retVal.Class += " active";
            return retVal;
        }
        
        public static MvcNavigationItem From(String text, String controller, ControllerActionRequestInformation requestInformation, String @class = default(String))
            => From(text, controller, "Index", requestInformation, @class);
    }
}
