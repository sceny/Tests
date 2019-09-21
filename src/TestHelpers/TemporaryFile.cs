using System;
using System.IO;

namespace Sceny.TestHelpers
{
    public class TemporaryFile: IDisposable
    {
        private FileInfo _fileInfo;
        private bool _disposed;

        public TemporaryFile() => FileName = Path.GetTempFileName();

        public TemporaryFile(string fileName) => FileName = fileName ?? throw new ArgumentNullException("fileName");

        public FileInfo FileInfo => _fileInfo ?? (_fileInfo = new FileInfo(FileName));

        public string FileName { get; private set; }

        public TemporaryFile ChangeExtensionTo(string extension)
        {
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(FileName);
            if (!string.IsNullOrWhiteSpace(extension) && !extension.StartsWith(".")) {
                extension = $".{extension}";
            }
            var newFileName = Path.Combine(FileInfo.DirectoryName, $"{fileNameWithoutExtension}{extension}");
            MoveTo(newFileName);
            return this;
        }

        public TemporaryFile MoveToCurrentDirectory()
        {
            var newFileName = Path.Combine(Directory.GetCurrentDirectory(), FileInfo.Name);
            MoveTo(newFileName);
            return this;
        }

        public TemporaryFile MoveTo(string newFileName)
        {
            File.Move(FileName, newFileName);
            FileName = newFileName;
            _fileInfo = null;
            return this;
        }

        public void WriteAllText(string text) => File.WriteAllText(FileName, text);

        public static implicit operator string(TemporaryFile temporaryFile)
            => temporaryFile != null ? temporaryFile.FileName : null;

        public static implicit operator TemporaryFile(string fileName)
            => new TemporaryFile(fileName);

        protected bool Equals(TemporaryFile other)
            => string.Equals(FileName, other.FileName);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TemporaryFile) obj);
        }

        public override int GetHashCode() => FileName?.GetHashCode() ?? 0;

        public override string ToString() => FileName;

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~TemporaryFile()
        {
            Dispose(false);
        }

        private void Dispose(bool isDisposing)
        {
            if (_disposed)
                return;

            try
            {
                if (File.Exists(FileName))
                    File.Delete(FileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            _disposed = true;
        }

        #endregion
    }
}
