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
            Text = text ?? throw new ArgumentNullException(nameof(text));
            Class = @class;
            IsActive = isActive;
        }
    }

    public class NavigationItem : _NavigationItem
    {
        public PathString Href { get; protected set; }

        internal NavigationItem(String text, String @class, Boolean isActive, PathString href)
            : base(text, @class, isActive)
            => Href = href;

        public NavigationItem(String text, PathString href, String @class = default(String), Boolean isActive = default(Boolean))
            : this(text, @class.SanitizeTo(default(String)), isActive, href) 
        { }

        public NavigationItem SetIsActiveIfMatches(String @string)
        {
            IsActive = Href.HasValue && 
                Href.Value.IsNotNullOrEmptyOrWhiteSpace() && 
                Href.Value.Matches(@string);

            if (IsActive)
                Class = Class.IsNull()
                    ? "active" :
                    $"{Class} active";

            return this;
        }

        public NavigationItem SetIsActiveIfMatches(PathString pathString)
            => SetIsActiveIfMatches(pathString.Value ?? default(String));

        public static NavigationItem From(String text, PathString href, String @class = default(String), Boolean isActive = default(Boolean))
            => new NavigationItem(text, href, @class, isActive);
    }

}
