namespace Sceny.TestHelpers.Tests
{
    public class TemporaryFileTests
    {
        [Fact]
        public void Disposing_temporary_file_deletes_the_file()
        {
            // assert
            const string fileName = "5544ff15-7768-4d53-9974-8088b5ecac9e.tmp";
            // act
            using (var file = new TemporaryFile(fileName))
            {
                File.WriteAllText(fileName, "Anything");
                file.FileName.Should().Be(fileName);
                File.Exists(fileName).Should().BeTrue();
            }
            // assert
            File.Exists(fileName).Should().BeFalse();
        }

        [Fact]
        public void Moving_file_tracks_the_new_file()
        {
            // assert
            const string fileName = "3156f567-b366-421a-923d-fd774eaf20fb.tmp";
            const string newFileName = "9cacdcdf-a520-43b2-b826-82bd083f3b87.txt";
            // act
            using (var file = new TemporaryFile(fileName))
            {
                File.WriteAllText(fileName, "Anything");
                File.Exists(fileName).Should().BeTrue();
                file.MoveTo(newFileName);
                // assert
                file.FileInfo.Name.Should().Be(newFileName);
                File.Exists(fileName).Should().BeFalse();
                File.Exists(newFileName).Should().BeTrue();
            }
            File.Exists(fileName).Should().BeFalse();
            File.Exists(newFileName).Should().BeFalse();
        }

        [Fact]
        public void Implicit_cast_from_string_to_temporary_file()
        {
            // arrange
            const string fileName = "/any/full/path/e94c6311-fc88-44ad-b225-947d879cbc7d.tmp";
            // act
            using TemporaryFile file = fileName;
            // assert
            file.FileName.Should().Be(fileName);
        }

        [Fact]
        public void Implicit_cast_temporary_file_to_string()
        {
            // arrange
            const string sourceFileName = "/any/full/path/9a97dac2-5726-4f44-8dac-7dfd5bf90f1b.tmp";
            var file = new TemporaryFile(sourceFileName);
            // act
            string fileName = file;
            // assert
            fileName.Should().Be(sourceFileName);
        }

        [Fact]
        public void Changing_file_name_extension_move_the_file_and_tracks_the_new_file()
        {
            // assert
            const string fileName = "de353a17-66ef-46f0-8a24-6ac4a4f6f89c.tmp";
            const string newExtensionFileName = "de353a17-66ef-46f0-8a24-6ac4a4f6f89c.txt";
            // act
            using (var file = new TemporaryFile(fileName))
            {
                File.WriteAllText(fileName, "Anything");
                File.Exists(fileName).Should().BeTrue();
                file.ChangeExtensionTo(".txt");
                // assert
                file.FileInfo.Name.Should().Be(newExtensionFileName);
                File.Exists(fileName).Should().BeFalse();
                File.Exists(newExtensionFileName).Should().BeTrue();
            }
            File.Exists(fileName).Should().BeFalse();
            File.Exists(newExtensionFileName).Should().BeFalse();
        }

        [Fact]
        public void Changing_file_name_extension_without_dot_add_dot_to_the_extension()
        {
            // assert
            const string fileName = "de353a17-66ef-46f0-8a24-6ac4a4f6f89c.tmp";
            const string newExtensionFileName = "de353a17-66ef-46f0-8a24-6ac4a4f6f89c.txt";
            // act
            using (var file = new TemporaryFile(fileName))
            {
                File.WriteAllText(fileName, "Anything");
                File.Exists(fileName).Should().BeTrue();
                file.ChangeExtensionTo("txt");
                // assert
                file.FileInfo.Name.Should().Be(newExtensionFileName);
                File.Exists(fileName).Should().BeFalse();
                File.Exists(newExtensionFileName).Should().BeTrue();
            }
            File.Exists(fileName).Should().BeFalse();
            File.Exists(newExtensionFileName).Should().BeFalse();
        }
    }
}