namespace ArtGallery.Data.Models;

/// <summary>
/// Представляет запись о денежном доходе.
/// </summary>
public class MoneyIncome
{
    /// <summary>
    /// Уникальный идентификатор записи о доходе.
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Идентификатор контрагента, связанного с доходом.
    /// </summary>
    public int CounterpartyId { get; set; }
    
    /// <summary>
    /// Сумма дохода.
    /// </summary>
    public decimal Amount { get; set; }
    
    /// <summary>
    /// Дата получения дохода.
    /// </summary>
    public DateTime IncomeDate { get; set; }
    
    /// <summary>
    /// Тип платежа (наличные, безналичные и т.д.).
    /// </summary>
    public string PaymentType { get; set; }
    
    /// <summary>
    /// Контрагент, связанный с доходом.
    /// </summary>
    public Counterparty Counterparty { get; set; }
}