using System;

namespace JasonPereira84.Helpers
{
    using Newtonsoft.Json;

    public struct RequestInformation
    {
        public String Id { get; set; }

        public String ContentType { get; set; }

        public String ContentLength { get; set; }

        public String Protocol { get; set; }

        public String Scheme { get; set; }

        public String Method { get; set; }

        public String Path { get; set; }

        public String QueryString { get; set; }

        public override String ToString()
            => JsonConvert.SerializeObject(this);
    }

}
