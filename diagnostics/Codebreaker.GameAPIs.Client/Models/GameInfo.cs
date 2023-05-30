using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
namespace ApiSdk.Models {
    public class GameInfo : IParsable {
        /// <summary>The duration property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public TimeSpanObject? Duration { get; set; }
#nullable restore
#else
        public TimeSpanObject Duration { get; set; }
#endif
        /// <summary>The gameId property</summary>
        public Guid? GameId { get; set; }
        /// <summary>The playerName property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? PlayerName { get; set; }
#nullable restore
#else
        public string PlayerName { get; set; }
#endif
        /// <summary>The startTime property</summary>
        public DateTimeOffset? StartTime { get; set; }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static GameInfo CreateFromDiscriminatorValue(IParseNode parseNode) {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new GameInfo();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        public IDictionary<string, Action<IParseNode>> GetFieldDeserializers() {
            return new Dictionary<string, Action<IParseNode>> {
                {"duration", n => { Duration = n.GetObjectValue<TimeSpanObject>(TimeSpanObject.CreateFromDiscriminatorValue); } },
                {"gameId", n => { GameId = n.GetGuidValue(); } },
                {"playerName", n => { PlayerName = n.GetStringValue(); } },
                {"startTime", n => { StartTime = n.GetDateTimeOffsetValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public void Serialize(ISerializationWriter writer) {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteObjectValue<TimeSpanObject>("duration", Duration);
            writer.WriteGuidValue("gameId", GameId);
            writer.WriteStringValue("playerName", PlayerName);
            writer.WriteDateTimeOffsetValue("startTime", StartTime);
        }
    }
}
