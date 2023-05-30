using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
namespace ApiSdk.Models {
    public class Game : IParsable {
        /// <summary>The duration property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public TimeSpanObject? Duration { get; set; }
#nullable restore
#else
        public TimeSpanObject Duration { get; set; }
#endif
        /// <summary>The endTime property</summary>
        public DateTimeOffset? EndTime { get; set; }
        /// <summary>The gameId property</summary>
        public Guid? GameId { get; private set; }
        /// <summary>The gameType property</summary>
        public ApiSdk.Models.GameType? GameType { get; set; }
        /// <summary>The holes property</summary>
        public int? Holes { get; private set; }
        /// <summary>The lastMoveNumber property</summary>
        public int? LastMoveNumber { get; set; }
        /// <summary>The maxMoves property</summary>
        public int? MaxMoves { get; private set; }
        /// <summary>The playerName property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? PlayerName { get; private set; }
#nullable restore
#else
        public string PlayerName { get; private set; }
#endif
        /// <summary>The startTime property</summary>
        public DateTimeOffset? StartTime { get; private set; }
        /// <summary>The won property</summary>
        public bool? Won { get; set; }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static Game CreateFromDiscriminatorValue(IParseNode parseNode) {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new Game();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        public IDictionary<string, Action<IParseNode>> GetFieldDeserializers() {
            return new Dictionary<string, Action<IParseNode>> {
                {"duration", n => { Duration = n.GetObjectValue<TimeSpanObject>(TimeSpanObject.CreateFromDiscriminatorValue); } },
                {"endTime", n => { EndTime = n.GetDateTimeOffsetValue(); } },
                {"gameId", n => { GameId = n.GetGuidValue(); } },
                {"gameType", n => { GameType = n.GetEnumValue<GameType>(); } },
                {"holes", n => { Holes = n.GetIntValue(); } },
                {"lastMoveNumber", n => { LastMoveNumber = n.GetIntValue(); } },
                {"maxMoves", n => { MaxMoves = n.GetIntValue(); } },
                {"playerName", n => { PlayerName = n.GetStringValue(); } },
                {"startTime", n => { StartTime = n.GetDateTimeOffsetValue(); } },
                {"won", n => { Won = n.GetBoolValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public void Serialize(ISerializationWriter writer) {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteObjectValue<TimeSpanObject>("duration", Duration);
            writer.WriteDateTimeOffsetValue("endTime", EndTime);
            writer.WriteEnumValue<GameType>("gameType", GameType);
            writer.WriteIntValue("lastMoveNumber", LastMoveNumber);
            writer.WriteBoolValue("won", Won);
        }
    }
}
