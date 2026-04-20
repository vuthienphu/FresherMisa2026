using System;
using System.Collections.Generic;
using System.Text;

namespace FresherMisa2026.Entities.Exception
{
    public class DuplicateKeyException : System.Exception
    {
        public string? ColumnName { get; }

        public DuplicateKeyException(string message, string? columnName = null) : base(message)
        {
            ColumnName = columnName;
        }
    }
}