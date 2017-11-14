using DotNetCommandLineParser.Core;
using DotNetCommandLineParser.Core.Exceptions;
using Xunit;

namespace Test
{
	public class CommandLineParserTests
    {
        [Fact]
        public void CanCreateCmdLineParser()
        {
			new CommandLineParser();
        }

		[Fact]
		public void CanCallWithNoParameters()
		{
			CommandLineParser parser = new CommandLineParser();
			MockObject parameters = parser.Parse<MockObject>(null);
			Assert.NotNull(parameters);
		}

		[Fact]
		public void CanParseArgsToMockObject()
		{
			string[] args = new string[]
			{
				"-X=13",
				"-Y HelloWorld"
			};

			CommandLineParser parser = new CommandLineParser();
			parser.AddParameter<int>("X", "-", "=", true, true, "Simple Integer");
			parser.AddParameter<string>("Y", "-", " ", true, true, "Simple String");
			MockObject parameters = parser.Parse<MockObject>(args);
			Assert.NotNull(parameters);
			Assert.Equal(parameters.X, 13);
			Assert.Equal(parameters.Y, "HelloWorld");
		}

	    [Fact]
	    public void ThrowsNotFoundErrorIfRequiredButMissing()
	    {
			string[] args = new string[]
			{
				"-X=13",
			};

			CommandLineParser parser = new CommandLineParser();
			parser.AddParameter<int>("X", "-", "=", true, true, "Simple Integer");
			parser.AddParameter<string>(
				name:"Y",
				prefix: "-",
				separator: " ",
				isRequired: true
			);
			Assert.Throws<ArgumentNotFoundException>( () => parser.Parse<MockObject>(args) );
		}

		[Fact]
		public void ThrowsArgumentWithoutValueExceptionForRequiredArgumentMissingValue()
		{
			string[] args = new string[]
			{
				"-X=13",
				"-Y"
			};

			CommandLineParser parser = new CommandLineParser();
			parser.AddParameter<int>("X", "-", "=", true, true, "Simple Integer");
			parser.AddParameter<string>("Y", "-", " ", true, true, "Simple String");
			Assert.Throws<ArgumentWithoutValueException>(() => parser.Parse<MockObject>(args));
		}

		[Fact]
		public void ThrowsArgumentWithoutValueExceptionForRequiredArgumentMissingValueWithTrailingWhitespace()
		{
			string[] args = new string[]
			{
				"-X=13",
				"-Y "
			};

			CommandLineParser parser = new CommandLineParser();
			parser.AddParameter<int>("X", "-", "=", true, true, "Simple Integer");
			parser.AddParameter<string>("Y", "-", " ", true, true, "Simple String");
			Assert.Throws<ArgumentWithoutValueException>(() => parser.Parse<MockObject>(args));
		}

		public class MockObject
		{
			public int X { get; set; }

			public string Y { get; set; }
		}
    }
}
