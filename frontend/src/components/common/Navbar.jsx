import React, { useContext } from 'react';
import { AuthContext } from '../../context/AuthContext';

const Navbar = () => {
  const { user, logout } = useContext(AuthContext);

  if (!user) return null;

  return (
    <nav className="navbar navbar-expand-lg navbar-dark bg-primary mb-4 shadow-sm">
      <div className="container">
        <span className="navbar-brand">User Management</span>
        <div className="d-flex align-items-center">
          <span className="text-white me-3">
            Hello, <strong>{user.name}</strong>
          </span>
          <button 
            onClick={logout}
            className="btn btn-outline-light btn-sm"
          >
            <i className="bi bi-box-arrow-right me-1"></i>
            Logout
          </button>
        </div>
      </div>
    </nav>
  );
};

export default Navbar;
