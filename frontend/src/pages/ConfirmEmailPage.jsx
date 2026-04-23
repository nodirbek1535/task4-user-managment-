import React, { useEffect, useState } from 'react';
import { useSearchParams, Link } from 'react-router-dom';
import { authApi } from '../api/authApi';

const ConfirmEmailPage = () => {
  const [searchParams] = useSearchParams();
  const token = searchParams.get('token');
  
  const [status, setStatus] = useState('loading'); // loading, success, error
  const [message, setMessage] = useState('');

  useEffect(() => {
    if (!token) {
      setStatus('error');
      setMessage('Invalid or missing confirmation token.');
      return;
    }

    const confirmToken = async () => {
      try {
        const res = await authApi.confirmEmail(token);
        setStatus('success');
        setMessage(res.data || 'Your email has been confirmed successfully!');
      } catch (error) {
        setStatus('error');
        setMessage(error.response?.data || 'Failed to confirm email. The link might be expired.');
      }
    };

    confirmToken();
  }, [token]);

  return (
    <div className="container-fluid vh-100 d-flex align-items-center justify-content-center bg-light">
      <div className="card shadow-sm border-0 text-center p-4" style={{ maxWidth: '500px', width: '100%' }}>
        <div className="card-body">
          {status === 'loading' && (
            <>
              <div className="spinner-border text-primary mb-3" role="status">
                <span className="visually-hidden">Loading...</span>
              </div>
              <h4>Confirming Email...</h4>
              <p className="text-muted">Please wait while we verify your token.</p>
            </>
          )}

          {status === 'success' && (
            <>
              <i className="bi bi-check-circle-fill text-success" style={{ fontSize: '3rem' }}></i>
              <h4 className="mt-3">Email Confirmed!</h4>
              <p className="text-muted">{message}</p>
              <Link to="/login" className="btn btn-primary mt-3 px-4">Go to Login</Link>
            </>
          )}

          {status === 'error' && (
            <>
              <i className="bi bi-x-circle-fill text-danger" style={{ fontSize: '3rem' }}></i>
              <h4 className="mt-3">Confirmation Failed</h4>
              <p className="text-muted">{message}</p>
              <Link to="/login" className="btn btn-outline-primary mt-3 px-4">Back to Login</Link>
            </>
          )}
        </div>
      </div>
    </div>
  );
};

export default ConfirmEmailPage;
