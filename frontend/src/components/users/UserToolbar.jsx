import React from 'react';

const UserToolbar = ({ selectedCount, onBlock, onUnblock, onDelete, onDeleteUnverified }) => {
  const isDisabled = selectedCount === 0;

  return (
    <div className="d-flex bg-light p-2 border rounded-top align-items-center">
      <div className="btn-group me-2" role="group">
        <button 
          type="button" 
          className="btn btn-outline-secondary d-flex align-items-center" 
          disabled={isDisabled} 
          onClick={onBlock}
          title="Block selected users"
        >
          <i className="bi bi-lock-fill me-1"></i> Block
        </button>
        <button 
          type="button" 
          className="btn btn-outline-secondary" 
          disabled={isDisabled} 
          onClick={onUnblock}
          title="Unblock selected users"
        >
          <i className="bi bi-unlock-fill text-success"></i>
        </button>
        <button 
          type="button" 
          className="btn btn-outline-secondary" 
          disabled={isDisabled} 
          onClick={onDelete}
          title="Delete selected users"
        >
          <i className="bi bi-trash3-fill text-danger"></i>
        </button>
        <button 
          type="button" 
          className="btn btn-outline-secondary" 
          disabled={isDisabled} 
          onClick={onDeleteUnverified}
          title="Delete selected unverified users"
        >
          <i className="bi bi-person-x-fill text-danger"></i>
        </button>
      </div>
      <span className="text-muted ms-auto pe-2 small">
        {selectedCount} item(s) selected
      </span>
    </div>
  );
};

export default UserToolbar;
