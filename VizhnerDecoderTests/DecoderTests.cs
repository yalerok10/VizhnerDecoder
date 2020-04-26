using Microsoft.VisualStudio.TestTools.UnitTesting;
using VizhnerDecoder;

namespace VizhnerDecoderTests
{
    [TestClass]
    public class DecoderTests
    {
        [TestMethod]
        public void EncodeStringTest1()
        {
            string sourceText = "привет, я текст теста .Net Standart 2.0!";
            string key = "весы";
            string result = "схъэжч, р нжпгн фйгнв .Net Standart 2.0!";
            Assert.AreEqual(result, Decoder.EncodeString(sourceText, key));
        }
        [TestMethod]
        public void EncodeStringTest2()
        {
            string sourceText = "привет, я текст теста с другим ключом!";
            string key = "рыбы";
            string result = "алйэхн, а нхётн гатнр м елдюйз ыжятяз!";
            Assert.AreEqual(result, Decoder.EncodeString(sourceText, key));
        }
        [TestMethod]
        public void DecodeStringTest()
        {
            string sourceText = "фазан";
            string key = "сила";
            string encodedText = "ёиуая";
            Assert.AreEqual(sourceText, Decoder.DecodeString(encodedText, key));
        }
        [TestMethod]
        public void DecodeStringTest2()
        {
            string sourceText = "моё предназначение - проверить букву \"ё\"";
            string key = "пепе";
            string encodedText = Decoder.EncodeString(sourceText, key);
            Assert.AreEqual(sourceText, Decoder.DecodeString(encodedText, key));
        }

    }
}
