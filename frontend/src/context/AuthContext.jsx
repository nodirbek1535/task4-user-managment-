import React, { createContext, useState, useEffect } from 'react';
import { authApi } from '../api/authApi';
import { toast } from 'react-toastify';
import { useNavigate } from 'react-router-dom';

export const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [user, setUser] = useState(null);
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();

  useEffect(() => {
    const storedUser = localStorage.getItem('user');
    const token = localStorage.getItem('token');
    
    if (storedUser && token) {
      setUser(JSON.parse(storedUser));
    }
    setLoading(false);
  }, []);

  const login = async (credentials) => {
    try {
      const response = await authApi.login(credentials);
      const { token, user: userData } = response.data;
      
      localStorage.setItem('token', token);
      localStorage.setItem('user', JSON.stringify(userData));
      setUser(userData);
      
      navigate('/');
      toast.success('Successfully logged in!');
      return true;
    } catch (error) {
      const message = error.response?.data || error.message || 'Login failed';
      toast.error(typeof message === 'string' ? message : JSON.stringify(message));
      return false;
    }
  };

  const register = async (data) => {
    try {
      await authApi.register(data);
      toast.success('Registration successful. Please check your email to confirm your account.');
      navigate('/login');
      return true;
    } catch (error) {
      const message = error.response?.data || error.message || 'Registration failed';
      toast.error(typeof message === 'string' ? message : JSON.stringify(message));
      return false;
    }
  };

  const logout = () => {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    setUser(null);
    navigate('/login');
  };

  return (
    <AuthContext.Provider value={{ user, login, register, logout, loading }}>
      {!loading && children}
    </AuthContext.Provider>
  );
};
