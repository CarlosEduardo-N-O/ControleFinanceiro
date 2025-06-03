import { BrowserRouter as Router, Routes, Route, useLocation } from 'react-router-dom';
import Dashboard from './pages/Dashboard';
import Login from './pages/Login';
import Usuarios from './pages/Usuarios';
import Contas from './pages/Contas';
import Categorias from './pages/Categorias';
import Transacoes from './pages/Transacoes';
import PrivateRoute from './components/PrivateRoute';
import BottomNav from './components/BottomNav';
import { useEffect, useState } from 'react';

const AppWrapper = () => {
  const location = useLocation();
  const [showMenu, setShowMenu] = useState(false);

  useEffect(() => {
    setShowMenu(location.pathname !== '/login');
  }, [location]);

  return (
    <>
      <Routes>
        <Route path="/login" element={<Login />} />
        <Route
          path="/"
          element={
            <PrivateRoute>
              <Dashboard />
            </PrivateRoute>
          }
        />
        <Route
          path="/usuarios"
          element={
            <PrivateRoute>
              <Usuarios />
            </PrivateRoute>
          }
        />
        <Route
          path="/contas"
          element={
            <PrivateRoute>
              <Contas />
            </PrivateRoute>
          }
        />
        <Route
          path="/categorias"
          element={
            <PrivateRoute>
              <Categorias />
            </PrivateRoute>
          }
        />
        <Route
          path="/transacoes"
          element={
            <PrivateRoute>
              <Transacoes />
            </PrivateRoute>
          }
        />
      </Routes>
      {showMenu && <BottomNav />}
    </>
  );
};

function App() {
  return (
    <Router>
      <AppWrapper />
    </Router>
  );
}

export default App;
