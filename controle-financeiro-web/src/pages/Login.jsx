import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../services/api';
import '../App.css'; // Importa os estilos

const Login = () => {
  const [login, setLogin] = useState('');
  const [senha, setSenha] = useState('');
  const navigate = useNavigate();

  const handleLogin = async (e) => {
    e.preventDefault();

    try {
      const response = await api.post('/Usuario/login', { login, senha });
      const usuario = response.data;

      if (usuario?.id && usuario?.login && usuario?.contas) {
        localStorage.setItem('usuario', JSON.stringify(usuario));
        navigate('/');
      } else {
        alert('Falha ao realizar login. Verifique seu usuário e senha.');
      }
    } catch (error) {
      if (error.response) {
        alert(`Erro da API: ${error.response.status} - ${error.response.data?.message || error.response.statusText}`);
      } else if (error.request) {
        alert('Nenhuma resposta da API. Verifique se o servidor está ligado.');
      } else {
        alert(`Erro: ${error.message}`);
      }
      console.error(error);
    }
  };

  return (
    <div className="login-container">
      <form onSubmit={handleLogin} className="login-form">
        <h2 className="login-title">Bem-vindo</h2>
        <p className="login-subtitle">Acesse com suas credenciais</p>

        <input
          type="text"
          placeholder="Login"
          value={login}
          onChange={(e) => setLogin(e.target.value)}
          required
          className="login-input"
        />

        <input
          type="password"
          placeholder="Senha"
          value={senha}
          onChange={(e) => setSenha(e.target.value)}
          required
          className="login-input"
        />

        <button type="submit" className="login-button">
          Entrar
        </button>
      </form>
    </div>
  );
};

export default Login;
