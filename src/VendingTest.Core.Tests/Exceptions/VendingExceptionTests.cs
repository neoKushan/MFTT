namespace VendingTest.Core.Tests.Exceptions
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using global::VendingTest.Core.Exceptions;
    using Xunit;

    public class VendingExceptionTests
    {
        [Fact]
        public void TestFileProcessException()
        {
            Exception ex =
                new VendingException(
                    "Message", new Exception("Inner exception."));

            // Save the full ToString() value, including the exception message and stack trace.
            var exceptionToString = ex.ToString();

            // Round-trip the exception: Serialize and de-serialize with a BinaryFormatter
            var bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                // "Save" object state
                bf.Serialize(ms, ex);

                // Re-use the same stream for de-serialization
                ms.Seek(0, 0);

                // Replace the original exception with de-serialized one
                ex = (VendingException)bf.Deserialize(ms);
            }

            // Double-check that the exception message and stack trace (owned by the base Exception) are preserved
            Assert.Equal(exceptionToString, ex.ToString());
        }
    }
}
