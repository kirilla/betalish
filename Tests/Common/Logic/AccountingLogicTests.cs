using Betalish.Common.Logic;

namespace Betalish.Tests.Common.Logic;

[TestClass]
public class AccountingLogicTests
{
    [DataTestMethod]
    [DataRow(0, 0, 0, 0)]
    [DataRow(100, 100, 0, 0)]
    [DataRow(100, 150, 0, 50)]
    [DataRow(200, 100, 100, 0)]
    [DataRow(-100, -50, 0, 50)]
    [DataRow(-50, -100, 50, 0)]
    [DataRow(-100, -100, 0, 0)]
    [DataRow(-100, 100, 0, 200)]
    [DataRow(100, -100, 200, 0)]
    public void Normalize_ReturnsExpectedResult(
        int debitInt, int creditInt, int expectedDebitInt, int expectedCreditInt)
    {
        decimal debit = debitInt;
        decimal credit = creditInt;
        decimal expectedDebit = expectedDebitInt;
        decimal expectedCredit = expectedCreditInt;

        var (normalizedDebit, normalizedCredit) = AccountingLogic.Normalize(debit, credit);

        Assert.AreEqual(expectedDebit, normalizedDebit, "Debit mismatch");
        Assert.AreEqual(expectedCredit, normalizedCredit, "Credit mismatch");
    }
}
