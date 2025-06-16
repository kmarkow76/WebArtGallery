namespace ArtGallery.Data.Models;

/// <summary>
/// Класс модели для сущности Counterparty.
/// Представляет контрагента, связанного с финансовыми операциями и арендой.
/// </summary>
public class Counterparty
{
    /// <summary>
    /// Уникальный идентификатор контрагента.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название или имя контрагента.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Номер телефона контрагента (может быть null).
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Адрес контрагента (может быть null).
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Электронная почта контрагента (может быть null).
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Дополнительная контактная информация контрагента (может быть null).
    /// </summary>
    public string? ContactInfo { get; set; }

    /// <summary>
    /// Список расходов, связанных с контрагентом.
    /// </summary>
    public List<MoneyExpense> MoneyExpenses { get; set; }

    /// <summary>
    /// Список доходов, связанных с контрагентом.
    /// </summary>
    public List<MoneyIncome> MoneyIncomes { get; set; }

    /// <summary>
    /// Список аренд, связанных с контрагентом.
    /// </summary>
    public List<Rental> Rentals { get; set; }
}