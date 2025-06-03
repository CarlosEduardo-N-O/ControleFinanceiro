import axios from 'axios';

const api = axios.create({
  baseURL: 'http://localhost:5195/api',
  headers: {
    'Content-Type': 'application/json'
  }
});

// Interceptor para resposta com erro
api.interceptors.response.use(
  response => response,
  error => {
    console.error('Erro da API:', error);
    return Promise.reject(error);
  }
);

export default api;
