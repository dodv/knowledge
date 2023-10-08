using Knowledge.Api.Common;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using OtpNet;
using Knowledge.Models.Models.DTOs;

namespace Knowledge.Api.Utils
{
    public static class AppUtil
    {
        private const string CodeChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public static string RandomAlphaNumericString(int length)
        {
            var random = new Random();
            var code = new char[length];

            for (var i = 0; i < code.Length; i++)
            {
                code[i] = CodeChars[random.Next(CodeChars.Length)];
            }

            return new string(code);
        }

        public static string Md5Digest(string content)
        {
            using var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(content));
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }

        public static async Task<PagedResults<T>> ToPagedResults<T>(this IQueryable<T> query, int page, int pageSize = KnowledgeConst.NumberItemOfListPage)
        {
            var list = await query
                        .Skip(page * pageSize)
                        .Take(pageSize + 1)
                        .ToListAsync();

            var pagedResults = new PagedResults<T>();
            if (list.Count == pageSize + 1)
            {
                pagedResults.HasMore = true;
                list.RemoveAt(pageSize);
            }

            pagedResults.Data = list;

            return pagedResults;
        }

        public static async Task<PagedResults<T>> ToPagedResults<T>(this List<T> data,
            int page,
            int pageSize = KnowledgeConst.NumberItemOfListPage)
        {
            var list = data
                .Skip(page * pageSize)
                .Take(pageSize + 1)
                .ToList();

            var pagedResults = new PagedResults<T>();
            if (list.Count == pageSize + 1)
            {
                pagedResults.HasMore = true;
                list.RemoveAt(pageSize);
            }

            pagedResults.Data = list;

            return pagedResults;
        }

        public static string RandomUrlSafeBase64String(int length)
        {
            var randBytes = new byte[length];
            using (var randoms = new RNGCryptoServiceProvider())
            {
                randoms.GetBytes(randBytes);
            }
            return WebEncoders.Base64UrlEncode(randBytes);
        }

        public static string RandomBase32String(int keyByteLength)
        {
            var randBytes = new byte[keyByteLength];
            using (var randoms = new RNGCryptoServiceProvider())
            {
                randoms.GetBytes(randBytes);
            }
            return Base32Encoding.ToString(randBytes);
        }

    }
}
