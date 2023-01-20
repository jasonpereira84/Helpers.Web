using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace JasonPereira84.Helpers
{
    using Misc = Extensions.Misc;

    using Newtonsoft.Json;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;

    public class Notification
    {
        [DefaultValue(warning)]
        public enum TypeEnum
        {
            danger = -2,
            warning = -1,
            info = 0,
            success = 2
        }

        [DefaultValue(pause)]
        public enum MouseOverEnum
        {
            pause = 1,
        }

        public struct placement
        {
            [DefaultValue(top)]
            public enum FromOptionsEnum
            {
                top = 1,
                bottom = -1,
            }

            [DefaultValue(right)]
            public enum AlignOptionsEnum
            {
                left = -1,
                center = 0,
                right = 1,
            }

            [JsonProperty("from")] public FromOptionsEnum From { get; set; }
            [JsonProperty("align")] public AlignOptionsEnum Align { get; set; }

            public static placement TopCenter
                => new placement
                {
                    From = FromOptionsEnum.top,
                    Align = AlignOptionsEnum.center
                };
        }

        public struct animate
        {
            [DefaultValue(fadeIn)]
            public enum OptionsEnum
            {
                bounce = 1,
                bounceIn = 2,
                bounceInDown = 3,
                bounceInLeft = 4,
                bounceInRight = 5,
                bounceInUp = 6,
                bounceOut = 7,
                bounceOutDown = 8,
                bounceOutLeft = 9,
                bounceOutRight = 10,
                bounceOutUp = 11,
                fadeIn = 12,
                fadeInDown = 13,
                fadeInDownBig = 14,
                fadeInLeft = 15,
                fadeInLeftBig = 16,
                fadeInRight = 17,
                fadeInRightBig = 18,
                fadeInUp = 19,
                fadeInUpBig = 20,
                fadeOut = 21,
                fadeOutDown = 22,
                fadeOutDownBig = 23,
                fadeOutLeft = 24,
                fadeOutLeftBig = 25,
                fadeOutRight = 26,
                fadeOutRightBig = 27,
                fadeOutUp = 28,
                fadeOutUpBig = 29,
                flash = 30,
                flipInX = 31,
                flipInY = 32,
                flipOutX = 33,
                flipOutY = 34,
                headShake = 35,
                heartBeat = 36,
                hinge = 37,
                jackInTheBox = 38,
                jello = 39,
                lightSpeedIn = 40,
                lightSpeedOut = 41,
                pulse = 42,
                rollIn = 43,
                rollOut = 44,
                rotateIn = 45,
                rotateInDownLeft = 46,
                rotateInDownRight = 47,
                rotateInUpLeft = 48,
                rotateInUpRight = 49,
                rotateOut = 50,
                rotateOutDownLeft = 51,
                rotateOutDownRight = 52,
                rotateOutUpLeft = 53,
                rotateOutUpRight = 54,
                rubberBand = 55,
                shake = 56,
                slideInDown = 57,
                slideInLeft = 58,
                slideInRight = 59,
                slideInUp = 60,
                slideOutDown = 61,
                slideOutLeft = 62,
                slideOutRight = 63,
                slideOutUp = 64,
                swing = 65,
                tada = 66,
                wobble = 67,
                zoomIn = 68,
                zoomInDown = 69,
                zoomInLeft = 70,
                zoomInRight = 71,
                zoomInUp = 72,
                zoomOut = 73,
                zoomOutDown = 74,
                zoomOutLeft = 75,
                zoomOutRight = 76,
                zoomOutUp = 77
            }

            [JsonProperty("enter")] public OptionsEnum Enter { get; set; }
            [JsonProperty("exit")] public OptionsEnum Exit { get; set; }
        }

        public struct options
        {
            [JsonProperty("icon")] public String Icon { get; set; }
            [JsonProperty("message")] public String Message { get; set; }

            public String AsJson()
                => JsonConvert.SerializeObject(this, new Newtonsoft.Json.Converters.StringEnumConverter());
        }

        public struct settings
        {
            [JsonProperty("type")] public TypeEnum Type { get; set; }
            [JsonProperty("allow_dismiss")] public Boolean AllowDismiss { get; set; }
            [JsonProperty("showProgressbar")] public Boolean ShowProgressbar { get; set; }
            [JsonProperty("delay")] public UInt32 Delay { get; set; }
            [JsonProperty("timer")] public UInt16 Timer { get; set; }
            [JsonProperty("mouse_over")] public MouseOverEnum? MouseOver { get; set; }

            [JsonProperty("element")] public String Element => "body";
            [JsonProperty("newest_on_top")] public Boolean NewestOnTop => true;
            [JsonProperty("placement")] public placement Placement => placement.TopCenter;
            [JsonProperty("template")]
            public String Template
                => @"<div data-notify=""container"" class=""col-xs-11 col-sm-3 alert alert-{0}"" role=""alert"">
                        <button type=""button"" aria-hidden=""true"" class=""close"" data-notify=""dismiss"">×</button>
                        <span data-notify=""message"" class=""mr-auto"">{2}</span>
                        <div class=""progress"" data-notify=""progressbar"">
                            <div class=""progress-bar progress-bar-{0}"" role=""progressbar"" aria-valuenow=""0"" aria-valuemin=""0"" aria-valuemax=""100"" style=""width: 0%;"">
                            </div>
                        </div>
                        <a href=""{3}"" target=""{4}"" data-notify=""url""></a>
                    </div>";

            public settings Sanitize()
            {
                if (AllowDismiss == false)
                {
                    if (Delay == 0)
                        throw new ArgumentOutOfRangeException(
                            $"{nameof(settings)}.{nameof(settings.AllowDismiss)}",
                            $"The value of '{nameof(settings)}.{nameof(settings.AllowDismiss)}' MUST-BE greater than zero (0) when {nameof(settings.AllowDismiss)} is 'false'.");
                    
                    MouseOver = MouseOverEnum.pause;
                }
                return this;
            }

            public String AsJson()
                => JsonConvert.SerializeObject(this, new Newtonsoft.Json.Converters.StringEnumConverter());
        }

        public class DecoratorResult<TActionResult> : IActionResult
            where TActionResult : IActionResult
        {
            public TActionResult Result { get; private set; }

            public Notification Notification { get; private set; }

            public DecoratorResult(TActionResult result, Notification notification)
            {
                Result = result ?? throw new ArgumentNullException(nameof(result));
                Notification = notification ?? throw new ArgumentNullException(nameof(notification));
            }

            public async Task ExecuteResultAsync(ActionContext actionContext)
            {
                if (actionContext == null)
                    throw new ArgumentNullException(nameof(actionContext));

                if (actionContext.HttpContext == null)
                    throw new ArgumentNullException($"{nameof(actionContext)}.{nameof(actionContext.HttpContext)}");

                var tempDataDictionaryFactory = actionContext
                    .HttpContext
                    .RequestServices
                    .GetService(typeof(ITempDataDictionaryFactory)) as ITempDataDictionaryFactory;
                if (tempDataDictionaryFactory == null)
                    throw new ArgumentNullException($"{nameof(actionContext)}.{nameof(ITempDataDictionaryFactory)}");

                var tempDataDictionary = tempDataDictionaryFactory.GetTempData(actionContext.HttpContext);
                if (tempDataDictionary == null)
                    throw new ArgumentNullException($"{nameof(actionContext)}.{nameof(ITempDataDictionary)}");

                var key = $"{typeof(Notification).FullName}";
                if (Misc.NotContainsKey(tempDataDictionary, key))
                    tempDataDictionary.Add(key, Notification.Jsons());
                else
                    tempDataDictionary[key] = Notification.Jsons();

                await Result.ExecuteResultAsync(actionContext);
            }
        }

        [JsonProperty("Options")]
        public options Options { get; private set; }

        [JsonProperty("Settings")]
        public settings Settings { get; private set; }

        public String Jsons() 
            => $"{Options.AsJson()}, {Settings.AsJson()}";

        public String AsJson()
                => JsonConvert.SerializeObject(this, new Newtonsoft.Json.Converters.StringEnumConverter());

        public Notification(options options, settings? settings = null)
        {
            Options = options;
            Settings = settings?.Sanitize() ?? new settings { AllowDismiss = true };
        }

        public static Notification Error(String message)
            => new Notification(
                new Notification.options
                {
                    Message = message
                },
                new Notification.settings
                {
                    Type = Notification.TypeEnum.danger,
                    AllowDismiss = true,
                    Delay = 0,
                    Timer = 0,
                });

        public static Notification Warning(String message)
            => new Notification(
                new Notification.options
                {
                    Message = message
                },
                new Notification.settings
                {
                    Type = Notification.TypeEnum.warning,
                    AllowDismiss = true,
                    Delay = 0,
                    Timer = 0,
                });

        public static Notification Success(String message)
            => new Notification(
                new Notification.options
                {
                    Message = message
                },
                new Notification.settings
                {
                    Type = Notification.TypeEnum.success,
                    AllowDismiss = true,
                    Delay = 1000,
                    Timer = 1000,
                });
    }
}
