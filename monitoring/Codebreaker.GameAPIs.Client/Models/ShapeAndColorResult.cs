using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
namespace ApiSdk.Models {
    public class ShapeAndColorResult : IParsable {
        /// <summary>The colorOrShape property</summary>
        public int? ColorOrShape { get; set; }
        /// <summary>The correct property</summary>
        public int? Correct { get; set; }
        /// <summary>The wrongPosition property</summary>
        public int? WrongPosition { get; set; }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static ShapeAndColorResult CreateFromDiscriminatorValue(IParseNode parseNode) {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new ShapeAndColorResult();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        public IDictionary<string, Action<IParseNode>> GetFieldDeserializers() {
            return new Dictionary<string, Action<IParseNode>> {
                {"colorOrShape", n => { ColorOrShape = n.GetIntValue(); } },
                {"correct", n => { Correct = n.GetIntValue(); } },
                {"wrongPosition", n => { WrongPosition = n.GetIntValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public void Serialize(ISerializationWriter writer) {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteIntValue("colorOrShape", ColorOrShape);
            writer.WriteIntValue("correct", Correct);
            writer.WriteIntValue("wrongPosition", WrongPosition);
        }
    }
}
