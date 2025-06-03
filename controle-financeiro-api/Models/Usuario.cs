﻿using ControleFinanceiroAPI.Models;

public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;

    public ICollection<Conta> Contas { get; set; } = new List<Conta>();
}
