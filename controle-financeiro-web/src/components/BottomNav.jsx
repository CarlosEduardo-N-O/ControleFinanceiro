import { Link, useLocation } from 'react-router-dom';
import { FaHome, FaUsers, FaWallet, FaTags, FaExchangeAlt } from 'react-icons/fa';
import '../App.css';

const BottomNav = () => {
  const location = useLocation();

  const menu = [
    { label: 'Início', path: '/', icon: <FaHome /> },
    { label: 'Usuários', path: '/usuarios', icon: <FaUsers /> },
    { label: 'Contas', path: '/contas', icon: <FaWallet /> },
    { label: 'Categorias', path: '/categorias', icon: <FaTags /> },
    { label: 'Transações', path: '/transacoes', icon: <FaExchangeAlt /> },
  ];

  return (
    <nav className="bottom-nav">
      {menu.map((item) => (
        <Link
          key={item.path}
          to={item.path}
          className={`nav-item ${location.pathname === item.path ? 'active' : ''}`}
        >
          <div className="nav-icon">{item.icon}</div>
          <div className="nav-label">{item.label}</div>
        </Link>
      ))}
    </nav>
  );
};

export default BottomNav;
