using SW.Framework.Validation;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;

namespace SW.Framework.Extensions
{
    /// <summary>
    ///     Class with <see cref="object" /> utilities.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        ///     Determines whether the specified value is null or default for that type.
        /// </summary>
        /// <param name="value">The value to be evaluated.</param>
        /// <returns><c>true</c> if the specified value is null or default; otherwise, <c>false</c>.</returns>
        public static bool IsNullOrDefault<T>(this T value)
        {
            return Equals(value, default(T));
        }

        /// <summary>
        ///     Determines whether the specified value is not null or default value for that type.
        /// </summary>
        /// <param name="value">The value to be evaluated.</param>
        /// <returns><c>true</c> if the specified value is not null or default value; otherwise, <c>false</c>.</returns>
        public static bool IsNotNullOrDefault<T>(this T value)
        {
            return !IsNullOrDefault(value);
        }

        /// <summary>
        ///     Deserialises the data on the provided byte array and reconstitutes the graph of objects.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialise and return.</typeparam>
        /// <param name="serializedObject">The byte array that contains the data to deserialize.</param>
        /// <returns>The top object of the deserialized graph.</returns>
        /// <exception cref="System.ArgumentNullException">the <paramref name="serializedObject" /> is null.</exception>
        public static T FromByteArray<T>(this byte[] serializedObject)
        {
            Check.NotNull(serializedObject, nameof(serializedObject));

            using (var memoryStream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                memoryStream.Write(serializedObject, 0, serializedObject.Length);
                memoryStream.Seek(0, SeekOrigin.Begin);
                var result = (T) formatter.Deserialize(memoryStream);
                return result;
            }
        }

        /// <summary>
        ///     Serialises an object, or graph of objects with the given root to a byte array.
        /// </summary>
        /// <param name="serializableObject">
        ///     The object, or root of the object graph, to serialise. All child objects of this root
        ///     object are automatically serialized.
        /// </param>
        /// <returns>A new byte array representing the serialized object.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="serializableObject" /> is null</exception>
        public static byte[] ToByteArray(this object serializableObject)
        {
            Check.NotNull(serializableObject, nameof(serializableObject));

            var formatter = new BinaryFormatter();
            using (var memoryStream = new MemoryStream())
            {
                formatter.Serialize(memoryStream, serializableObject);
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        ///     Creates a deep clone of the object using binary serialisation.
        /// </summary>
        /// <typeparam name="T">The type of the object to be cloned.</typeparam>
        /// <param name="item">The object to be cloned.</param>
        /// <returns>The clone of the object.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="item" /> is null. </exception>
        public static T DeepClone<T>(this T item)
        {
            Check.NotNull(item, nameof(item));
            return item.ToByteArray().FromByteArray<T>();
        }

        /// <summary>
        ///     A T extension method that shallow copy.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="item">The item to act on.</param>
        /// <returns>A shallow copy of the object.</returns>
        public static T ShallowCopy<T>(this T item)
        {
            Check.NotNull(item, nameof(item));

            var method = item.GetType().GetMethod("MemberwiseClone", BindingFlags.NonPublic | BindingFlags.Instance);
            return (T) method.Invoke(item, null);
        }

        /// <summary>
        ///     Ensures that the object has a value. If the object passed in has a null value,
        ///     the creator function is used to return a value.
        /// </summary>
        /// <typeparam name="T">The type of the object to ensure a value for. Has to be a <c>class</c>.</typeparam>
        /// <param name="item">The object for which the value is to be ensured.</param>
        /// <param name="creator">A function that created an object of type <typeparamref name="T" /> if the object is null.</param>
        /// <returns>The object instance created.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="creator" /> is null</exception>
        public static T Ensure<T>(this T item, Func<T> creator) where T : class
        {
            Check.NotNull(creator, nameof(creator));
            return item ?? creator.Invoke();
        }

        /// <summary>
        ///     Gets the property value from an object instance.
        /// </summary>
        /// <typeparam name="T">The type of the property to retrieve the value from.</typeparam>
        /// <param name="source">The source object.</param>
        /// <param name="propertyName">Name of the property to retrieve the value from.</param>
        /// <returns>The property value if the relevant property is found on the object.</returns>
        /// <exception cref="System.ArgumentNullException">
        ///     either <paramref name="source" /> or <paramref name="propertyName" /> is
        ///     <c>null</c>
        /// </exception>
        /// <exception cref="System.ArgumentException">
        ///     a property with the specified property name is not found on the source
        ///     object.
        /// </exception>
        public static T GetPropertyValue<T>(this object source, string propertyName)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNullOrWhitespace(propertyName, nameof(propertyName));

            var sourceType = source.GetType();
            var propertyInfo = sourceType.GetProperty(propertyName);

            if (propertyInfo == null)
            {
                const string errorText = "The property '{0}' does not exist on the source object supplied.";
                var errorMessage = string.Format(errorText, propertyName);
                throw new ArgumentException(errorMessage, propertyName);
            }

            var propertyValue = propertyInfo.GetValue(source);
            return propertyValue != null ? (T) propertyValue : default(T);
        }

        /// <summary>
        ///     Serialises an object to XML and places the content into a <see cref="Stream" />.
        /// </summary>
        /// <param name="objectToSerialize">The object to serialize.</param>
        /// <param name="stream">The <see cref="Stream" /> where the serialised object content will be inserted.</param>
        /// <exception cref="System.ArgumentNullException">
        ///     either <paramref name="objectToSerialize" /> or
        ///     <paramref name="stream" /> is <c>null</c>
        /// </exception>
        public static void ToXml(this object objectToSerialize, Stream stream)
        {
            Check.NotNull(objectToSerialize, nameof(objectToSerialize));
            Check.NotNull(stream, nameof(stream));

            new XmlSerializer(objectToSerialize.GetType()).Serialize(stream, objectToSerialize);
        }

        /// <summary>
        ///     Serialises an object to XML and places the content into a <see cref="TextWriter" />.
        /// </summary>
        /// <param name="objectToSerialize">The object to serialize.</param>
        /// <param name="writer">The <see cref="TextWriter" /> where the serialised object content will be inserted.</param>
        /// <exception cref="System.ArgumentNullException">
        ///     either <paramref name="objectToSerialize" /> or
        ///     <paramref name="writer" /> is <c>null</c>
        /// </exception>
        public static void ToXml(this object objectToSerialize, TextWriter writer)
        {
            new XmlSerializer(objectToSerialize.GetType()).Serialize(writer, objectToSerialize);
        }

        /// <summary>
        ///     Serialises an object to an XML string.
        /// </summary>
        /// <param name="objectToSerialize">The object to serialize.</param>
        /// <returns>An XML representation of the serialised object.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="objectToSerialize" /> is null</exception>
        public static string ToXmlString(this object objectToSerialize)
        {
            Check.NotNull(objectToSerialize, nameof(objectToSerialize));

            using (var writer = new StringWriter())
            {
                objectToSerialize.ToXml(writer);
                return writer.ToString();
            }
        }

        /// <summary>
        ///     Deserialises an object from XML string representing the serialised object.
        /// </summary>
        /// <typeparam name="T">The type of the object to be serialised.</typeparam>
        /// <param name="xmlString">The XML string representing the object in its serialised state.</param>
        /// <returns>T.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="xmlString" /> is <c>null</c></exception>
        /// <exception cref="System.ArgumentException">
        ///     The XML string could not be deserialized into the specified type
        ///     <typeparamref name="T" />.
        /// </exception>
        public static T DeserialiseFromXmlString<T>(this string xmlString) where T : class
        {
            Check.NotNullOrWhitespace(xmlString, nameof(xmlString));

            var serializer = new XmlSerializer(typeof(T));
            var xmlReader = XmlReader.Create(new StringReader(xmlString));

            if (serializer.CanDeserialize(xmlReader))
            {
                object result;
                using (TextReader reader = new StringReader(xmlString))
                {
                    result = serializer.Deserialize(reader);
                }

                return (T) result;
            }
            const string errorText = "The XML string could not be deserialized.";
            throw new ArgumentException(errorText, nameof(xmlString));
        }

        /// <summary>
        ///     Safely casts the object ot he specified target type.
        /// </summary>
        /// <typeparam name="TTarget">The type of the t target.</typeparam>
        /// <param name="item">The item.</param>
        /// <returns>TTarget.</returns>
        /// <exception cref="System.NotSupportedException">The object is not convertible to to the target type specified.</exception>
        public static TTarget AsType<TTarget>(this object item) where TTarget : class
        {
            var target = item as TTarget;
            if (target == null)
                throw new NotSupportedException(
                    $"The object of type {item.GetType().FullName} is not convertible to type {typeof(TTarget).FullName}.");
            return target;
        }
    }
}