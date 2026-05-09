using Xunit;

namespace Pluma.Library.InternalServiceAuth.Tests;

public class InternalServiceSecretComparerTests
{
    [Fact]
    public void EqualsUtf8_same_secret_returns_true()
    {
        Assert.True(InternalServiceSecretComparer.EqualsUtf8("abc", "abc"));
    }

    [Fact]
    public void EqualsUtf8_different_returns_false()
    {
        Assert.False(InternalServiceSecretComparer.EqualsUtf8("abc", "abd"));
    }

    [Fact]
    public void EqualsUtf8_different_length_returns_false()
    {
        Assert.False(InternalServiceSecretComparer.EqualsUtf8("a", "ab"));
    }
}
