using System;

namespace JasonPereira84.Helpers
{
    public interface IServiceContext<TData>
    {
        String Type { get; }

        String Serializer { get; }

        TData Data { get; }
    }

    public abstract class _ServiceContext<TData> : IServiceContext<TData>
    {
        protected _ServiceContext(String type, String serializer, TData data)
        {
            Type = type.IsSane(out String resultType) ?? throw new ArgumentOutOfRangeException($"'{nameof(IServiceContext<TData>.Type)}' CANNOT-BE-{resultType}");
            Serializer = serializer.IsSane(out String resultSerializer) ?? throw new ArgumentOutOfRangeException($"'{nameof(IServiceContext<TData>.Serializer)}' CANNOT-BE-{resultSerializer}");
            Data = data;
        }

        public String Type { get; protected set; }

        public String Serializer { get; protected set; }

        public TData Data { get; protected set; }
    }

}
