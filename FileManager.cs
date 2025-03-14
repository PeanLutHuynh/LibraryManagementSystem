﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace LibraryManagementSystem
{
    // Singleton Pattern
    public class FileManager
    {
        // Singleton instance
        private static FileManager instance;
        
        // Private constructor (Singleton pattern)
        private FileManager()
        {
        }

        // Singleton instance property
        public static FileManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FileManager();
                }
                return instance;
            }
        }

        // Method to serialize library object to file
        public void SerializeLibrary(Library library, string filePath)
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(library, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, jsonString);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Method to deserialize library object from file
        // Method to deserialize library object from file
        public Library DeserializeLibrary(string filePath)
        {
            try
            {
                string jsonString = File.ReadAllText(filePath);

                // Tạo một lớp tạm để deserialize dữ liệu
                var libraryData = JsonSerializer.Deserialize<LibraryData>(jsonString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (libraryData != null)
                {
                    // Cập nhật dữ liệu vào instance hiện tại
                    Library.Instance.Books = libraryData.Books;
                    Library.Instance.Users = libraryData.Users;
                    Library.Instance.CurrentUser = libraryData.CurrentUser;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi đọc dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return Library.Instance;
        }

        // Lớp tạm để deserialize dữ liệu
        [Serializable]
        public class LibraryData
        {
            public List<Book> Books { get; set; }
            public List<User> Users { get; set; }
            public User CurrentUser { get; set; }
        }

    }
}