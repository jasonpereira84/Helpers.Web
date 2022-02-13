using System;

namespace JasonPereira84.Helpers
{
    using Misc = Extensions.Misc;

    public interface IServiceContext<TData>
    {
        String Type { get; }

        String Serializer { get; }

        TData Data { get; }
    }

    public abstract class _ServiceContext<TData> : IServiceContext<TData>
    {
        public String Type { get; protected set; }

        public String Serializer { get; protected set; }

        public TData Data { get; protected set; }

        protected _ServiceContext(String type, String serializer, TData data)
        {
            if (!Misc.EvaluateSanity(type, out String saneType))
                throw new ArgumentException($"The value of '{nameof(IServiceContext<TData>.Type)}' cannot be an empty string.", nameof(IServiceContext<TData>.Type));
            Type = saneType;

            if (!Misc.EvaluateSanity(serializer, out String saneSerializer))
                throw new ArgumentException($"The value of '{nameof(IServiceContext<TData>.Serializer)}' cannot be an empty string.", nameof(IServiceContext<TData>.Serializer));
            Serializer = saneSerializer;

            Data = data;
        }

    }

}
