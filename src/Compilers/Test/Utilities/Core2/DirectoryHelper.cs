﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.CodeAnalysis.Text;

namespace Microsoft.CodeAnalysis.Test.Utilities
{
    public class DirectoryHelper
    {
        public event Action<string> FileFound;

        private readonly string m_rootPath = null;
        public DirectoryHelper(string path)
        {
            if (!Directory.Exists(path))
            {
                throw new ArgumentException("Directory '" + path + "' does not exist.", "path");
            }

            m_rootPath = path;
        }

        public void IterateFiles(string[] searchPatterns)
        {
            IterateFiles(searchPatterns, m_rootPath);
        }

        private void IterateFiles(string[] searchPatterns, string directoryPath)
        {
            var files = new List<string>();
            foreach (var pattern in searchPatterns)
            {
                files.AddRange(Directory.GetFiles(directoryPath, pattern, SearchOption.TopDirectoryOnly));
            }

            foreach (var f in files)
            {
                FileFound(f);
            }

            foreach (var d in Directory.GetDirectories(directoryPath))
            {
                IterateFiles(searchPatterns, d);
            }
        }
    }
}
