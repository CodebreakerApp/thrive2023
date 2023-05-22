using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
namespace ApiSdk.Models {
    public class SetMoveResponse : IParsable {
        /// <summary>The colorResult property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public ApiSdk.Models.ColorResult? ColorResult { get; set; }
#nullable restore
#else
        public ApiSdk.Models.ColorResult ColorResult { get; set; }
#endif
        /// <summary>The gameId property</summary>
        public Guid? GameId { get; set; }
        /// <summary>The gameType property</summary>
        public ApiSdk.Models.GameType? GameType { get; set; }
        /// <summary>The moveNumber property</summary>
        public int? MoveNumber { get; set; }
        /// <summary>The shapeResult property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public ShapeAndColorResult? ShapeResult { get; set; }
#nullable restore
#else
        public ShapeAndColorResult ShapeResult { get; set; }
#endif
        /// <summary>The simpleResult property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public SimpleColorResult? SimpleResult { get; set; }
#nullable restore
#else
        public SimpleColorResult SimpleResult { get; set; }
#endif
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static SetMoveResponse CreateFromDiscriminatorValue(IParseNode parseNode) {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new SetMoveResponse();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        public IDictionary<string, Action<IParseNode>> GetFieldDeserializers() {
            return new Dictionary<string, Action<IParseNode>> {
                {"colorResult", n => { ColorResult = n.GetObjectValue<ApiSdk.Models.ColorResult>(ApiSdk.Models.ColorResult.CreateFromDiscriminatorValue); } },
                {"gameId", n => { GameId = n.GetGuidValue(); } },
                {"gameType", n => { GameType = n.GetEnumValue<GameType>(); } },
                {"moveNumber", n => { MoveNumber = n.GetIntValue(); } },
                {"shapeResult", n => { ShapeResult = n.GetObjectValue<ShapeAndColorResult>(ShapeAndColorResult.CreateFromDiscriminatorValue); } },
                {"simpleResult", n => { SimpleResult = n.GetObjectValue<SimpleColorResult>(SimpleColorResult.CreateFromDiscriminatorValue); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public void Serialize(ISerializationWriter writer) {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteObjectValue<ApiSdk.Models.ColorResult>("colorResult", ColorResult);
            writer.WriteGuidValue("gameId", GameId);
            writer.WriteEnumValue<GameType>("gameType", GameType);
            writer.WriteIntValue("moveNumber", MoveNumber);
            writer.WriteObjectValue<ShapeAndColorResult>("shapeResult", ShapeResult);
            writer.WriteObjectValue<SimpleColorResult>("simpleResult", SimpleResult);
        }
    }
}
