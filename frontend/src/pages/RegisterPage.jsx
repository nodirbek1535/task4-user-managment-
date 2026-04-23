import React, { useState, useContext } from 'react';
import { Link } from 'react-router-dom';
import { AuthContext } from '../context/AuthContext';

const RegisterPage = () => {
  const { register } = useContext(AuthContext);
  const [formData, setFormData] = useState({ name: '', email: '', password: '' });
  const [isSubmitting, setIsSubmitting] = useState(false);

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!formData.name || !formData.email || !formData.password) return;
    
    setIsSubmitting(true);
    await register(formData);
    setIsSubmitting(false);
  };

  return (
    <div className="container-fluid vh-100 d-flex align-items-center justify-content-center bg-light">
      <div className="card shadow-sm border-0" style={{ maxWidth: '400px', width: '100%' }}>
        <div className="card-body p-4 p-md-5">
          <div className="text-center mb-4">
            <h2 className="fw-bold text-primary mb-2">THE APP</h2>
            <p className="text-muted small">Create your account</p>
          </div>
          
          <form onSubmit={handleSubmit}>
            <div className="form-floating mb-3">
              <input 
                type="text" 
                className="form-control" 
                id="name" 
                name="name"
                placeholder="John Doe"
                value={formData.name}
                onChange={handleChange}
                required
                maxLength="100"
              />
              <label htmlFor="name">Full Name</label>
            </div>

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
              <label htmlFor="password">Password (Any character)</label>
            </div>
            
            <button 
              type="submit" 
              className="btn btn-primary w-100 py-2 fw-bold"
              disabled={isSubmitting || !formData.name || !formData.email || !formData.password}
            >
              {isSubmitting ? 'Registering...' : 'Sign Up'}
            </button>
          </form>
          
          <div className="text-center mt-4 small">
            <span className="text-muted">Already have an account? </span>
            <Link to="/login" className="text-decoration-none">Sign in</Link>
          </div>
        </div>
      </div>
    </div>
  );
};

export default RegisterPage;
