using System;
using System.Text;

namespace VizhnerDecoder
{
    public static class Decoder
    {

        // Для каждого символа строки, если он является буквой русского алфавита,
        // вызывается метод GetEncodedChar, возвращающий соответствующую букву согласно шифру Вижнера,
        // при этом указатель на текущую букву ключа увеличивается на единицу
        public static string EncodeString(string source, string key)
        {
            int k = 0;
            var sb = new StringBuilder();
            key = key.Trim().ToLower();
            for (int i = 0; i < source.Length; i++)
            {
                if (((source[i] >= 1072) && (source[i] <= 1103)) || (source[i] == 1105))
                {
                    sb.Append(GetEncodedChar(source[i], key[k]));
                    k = ++k == key.Length ? 0 : k;
                }
                else sb.Append(source[i]);

            }
            return sb.ToString();
        }

        // Вычисляется позиция кодируемой буквы относительно "а", тогда закодированная
        // буква будет на этой позиции относительно буквы ключа
        // Если полученный символ больше значения "я", то вычисляется его положение
        // относительно "а"
        // Если получена буква ё, передаём ей соответствующий символ
        private static char GetEncodedChar(char v1, char v2)
        {

            v1 = CharNumber(v1);
            v2 = CharNumber(v2);
            int difference = v1 - 1072;
            int result = v2 + difference;
            if (result > 1104)
            {
                int dif = result - 1104;
                result = 1071 + dif;
            }
            if (result >= 1078)
            {
                if (result == 1078) result = 1105;
                else --result;
            }
            return (char)result;
        }

        // Для каждого символа строки, если он является буквой русского алфавита,
        // вызывается метод GetDecodedChar, возвращающий соответствующую букву согласно шифру Вижнера,
        // при этом указатель на текущую букву ключа увеличивается на единицу
        public static string DecodeString(string s, string key)
        {
            int k = 0;
            var sb = new StringBuilder();
            key = key.Trim().ToLower();

            for (int i = 0; i < s.Length; i++)
            {
                if (((s[i] >= 1072) && (s[i] <= 1103)) || (s[i] == 1105))
                {
                    sb.Append(GetDecodedChar(s[i], key[k]));
                    k = ++k == key.Length ? 0 : k;
                }
                else sb.Append(s[i]);

            }
            return sb.ToString();
        }

        // Если символ ключа больше символа строки, положение декодируемой буквы
        // вычесляется как сумма разности "я" и символа ключа и разности символа строки
        // и "а", значение самой буквы вычисляется как "а" + положение
        // Если получена буква ё, передаём ей соответствующий символ
        private static char GetDecodedChar(char v1, char v2)
        {
            v1 = CharNumber(v1);
            v2 = CharNumber(v2);
            int difference = v1 - v2;
            if (difference < 0)
            {
                difference = (1104 - v2) + (v1 - 1072) + 1;
            }
            int CurrentChar = 1072 + difference;
            if (CurrentChar >= 1078)
            {
                if (CurrentChar == 1078) CurrentChar = 1105;
                else --CurrentChar;
            }
            return (char)CurrentChar;
        }


        // Т.к. "ё" кодируется как 1105, решила вставить её в последовательность
        // букв по порядку и сдвинуть все остальные символы на единицу для удобства
        // работы с ними
        private static char CharNumber(char v1)
        {
            if (v1 == 1105)
            {
                return (char)1078;
            }
            if (v1 > 1077)
            {
                return ++v1;
            }
            return v1;
        }
    }
}
