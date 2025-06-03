import { useEffect, useState } from 'react';
import api from '../services/api';
import '../App.css';

const Dashboard = () => {
  const [contas, setContas] = useState([]);

  useEffect(() => {
    const fetchContas = async () => {
      try {
        const usuario = JSON.parse(localStorage.getItem('usuario'));
        const usuarioId = usuario?.id;

        if (!usuarioId) {
          console.error('Usuário não encontrado no localStorage.');
          return;
        }

        const response = await api.get('/Contas', {
          params: { usuario: usuarioId }
        });

        setContas(response.data);
      } catch (error) {
        console.error('Erro ao buscar contas:', error);
      }
    };

    fetchContas();
  }, []);

  return (
    <div className="dashboard-container">
      <h2 className="dashboard-title">Minhas Contas</h2>
      <div className="contas-list">
        {contas.map((conta) => (
          <div key={conta.id} className="conta-card">
            <div className="conta-nome">{conta.nome}</div>
            <div className="conta-saldo">R$ {Number(conta.saldo).toFixed(2)}</div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default Dashboard;
