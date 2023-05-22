using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
namespace ApiSdk.Models {
    public class SetMoveRequest : IParsable {
        /// <summary>The colorGuessPegs property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<ColorField>? ColorGuessPegs { get; set; }
#nullable restore
#else
        public List<ColorField> ColorGuessPegs { get; set; }
#endif
        /// <summary>The gameId property</summary>
        public Guid? GameId { get; set; }
        /// <summary>The gameType property</summary>
        public ApiSdk.Models.GameType? GameType { get; set; }
        /// <summary>The moveNumber property</summary>
        public int? MoveNumber { get; set; }
        /// <summary>The playerName property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? PlayerName { get; set; }
#nullable restore
#else
        public string PlayerName { get; set; }
#endif
        /// <summary>The shapeGuessPegs property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<ShapeAndColorField>? ShapeGuessPegs { get; set; }
#nullable restore
#else
        public List<ShapeAndColorField> ShapeGuessPegs { get; set; }
#endif
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static SetMoveRequest CreateFromDiscriminatorValue(IParseNode parseNode) {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new SetMoveRequest();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        public IDictionary<string, Action<IParseNode>> GetFieldDeserializers() {
            return new Dictionary<string, Action<IParseNode>> {
                {"colorGuessPegs", n => { ColorGuessPegs = n.GetCollectionOfObjectValues<ColorField>(ColorField.CreateFromDiscriminatorValue)?.ToList(); } },
                {"gameId", n => { GameId = n.GetGuidValue(); } },
                {"gameType", n => { GameType = n.GetEnumValue<GameType>(); } },
                {"moveNumber", n => { MoveNumber = n.GetIntValue(); } },
                {"playerName", n => { PlayerName = n.GetStringValue(); } },
                {"shapeGuessPegs", n => { ShapeGuessPegs = n.GetCollectionOfObjectValues<ShapeAndColorField>(ShapeAndColorField.CreateFromDiscriminatorValue)?.ToList(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public void Serialize(ISerializationWriter writer) {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteCollectionOfObjectValues<ColorField>("colorGuessPegs", ColorGuessPegs);
            writer.WriteGuidValue("gameId", GameId);
            writer.WriteEnumValue<GameType>("gameType", GameType);
            writer.WriteIntValue("moveNumber", MoveNumber);
            writer.WriteStringValue("playerName", PlayerName);
            writer.WriteCollectionOfObjectValues<ShapeAndColorField>("shapeGuessPegs", ShapeGuessPegs);
        }
    }
}
