using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
namespace ApiSdk.Models {
    public class GetGamesRankResponse : IParsable {
        /// <summary>The date property</summary>
        public Date? Date { get; set; }
        /// <summary>The games property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<GameInfo>? Games { get; set; }
#nullable restore
#else
        public List<GameInfo> Games { get; set; }
#endif
        /// <summary>The gameType property</summary>
        public ApiSdk.Models.GameType? GameType { get; set; }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static GetGamesRankResponse CreateFromDiscriminatorValue(IParseNode parseNode) {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new GetGamesRankResponse();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        public IDictionary<string, Action<IParseNode>> GetFieldDeserializers() {
            return new Dictionary<string, Action<IParseNode>> {
                {"date", n => { Date = n.GetDateValue(); } },
                {"games", n => { Games = n.GetCollectionOfObjectValues<GameInfo>(GameInfo.CreateFromDiscriminatorValue)?.ToList(); } },
                {"gameType", n => { GameType = n.GetEnumValue<GameType>(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public void Serialize(ISerializationWriter writer) {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteDateValue("date", Date);
            writer.WriteCollectionOfObjectValues<GameInfo>("games", Games);
            writer.WriteEnumValue<GameType>("gameType", GameType);
        }
    }
}
