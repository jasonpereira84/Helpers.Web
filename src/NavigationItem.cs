using System;
using System.Linq;
using System.Collections.Generic;

namespace JasonPereira84.Helpers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    using Newtonsoft.Json;

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

        public static MvcNavigationItem From(String text, String controller, String action, MvcRequestInformation requestInformation, String @class = default(String))
            => new MvcNavigationItem(text, controller, action, @class)
                .Do(item =>
                {
                    item.IsActive = requestInformation.Controller.IsNotNullOrEmptyOrWhiteSpace() && requestInformation.Action.IsNotNullOrEmptyOrWhiteSpace()
                        && item.Controller.IsNotNullOrEmptyOrWhiteSpace() && item.Controller.Matches(requestInformation.Controller)
                        && item.Action.IsNotNullOrEmptyOrWhiteSpace() && item.Action.Matches(requestInformation.Action);
                })
                .If(item => item.IsActive,
                    item => { item.Class = item.Class + " active"; return item; });
        public static MvcNavigationItem From(String text, String controller, MvcRequestInformation requestInformation, String @class = default(String))
            => From(text, controller, "Index", requestInformation, @class);
    }
}
