namespace ArtGallery.Data.Models;

/// <summary>
/// Представляет запись о денежном расходе.
/// </summary>
public class MoneyExpense
{
    /// <summary>
    /// Уникальный идентификатор записи о расходе.
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Идентификатор контрагента, связанного с расходом.
    /// </summary>
    public int CounterpartyId { get; set; }
    
    /// <summary>
    /// Идентификатор статьи расхода.
    /// </summary>
    public int ExpenseArticleId { get; set; }
    
    /// <summary>
    /// Дата совершения расхода.
    /// </summary>
    public DateTime ExpenseDate { get; set; }
    
    /// <summary>
    /// Сумма расхода.
    /// </summary>
    public decimal Sum { get; set; }
    
    /// <summary>
    /// Контрагент, связанный с расходом.
    /// </summary>
    public Counterparty Counterparty { get; set; }
    
    /// <summary>
    /// Статья расхода.
    /// </summary>
    public ExpenseArticle ExpenseArticle { get; set; }
}