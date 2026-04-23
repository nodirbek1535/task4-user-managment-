import React, { useState, useContext } from 'react';
import { Link } from 'react-router-dom';
import { AuthContext } from '../context/AuthContext';

const LoginPage = () => {
  const { login } = useContext(AuthContext);
  const [formData, setFormData] = useState({ email: '', password: '' });
  const [isSubmitting, setIsSubmitting] = useState(false);

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!formData.email || !formData.password) return;
    
    setIsSubmitting(true);
    await login(formData);
    setIsSubmitting(false);
  };

  return (
    <div className="container-fluid vh-100 d-flex align-items-center justify-content-center bg-light">
      <div className="card shadow-sm border-0" style={{ maxWidth: '400px', width: '100%' }}>
        <div className="card-body p-4 p-md-5">
          <div className="text-center mb-4">
            <h2 className="fw-bold text-primary mb-2">THE APP</h2>
            <p className="text-muted small">Sign In to The App</p>
          </div>
          
          <form onSubmit={handleSubmit}>
            <div className="form-floating mb-3">
              <input 
                type="email" 
                className="form-control" 
                id="email" 
                name="email"
                placeholder="name@example.com"
                value={formData.email}
                onChange={handleChange}
                required
              />
              <label htmlFor="email">E-mail</label>
            </div>
            
            <div className="form-floating mb-4">
              <input 
                type="password" 
                className="form-control" 
                id="password" 
                name="password"
                placeholder="Password"
                value={formData.password}
                onChange={handleChange}
                required
              />
              <label htmlFor="password">Password</label>
            </div>
            
            <button 
              type="submit" 
              className="btn btn-primary w-100 py-2 fw-bold"
              disabled={isSubmitting || !formData.email || !formData.password}
            >
              {isSubmitting ? 'Signing in...' : 'Sign In'}
            </button>
          </form>
          
          <div className="d-flex justify-content-between mt-4 small">
            <div>
              <span className="text-muted">Don't have an account? </span>
              <Link to="/register" className="text-decoration-none">Sign up</Link>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default LoginPage;
