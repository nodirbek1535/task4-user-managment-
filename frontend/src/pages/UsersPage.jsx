import React, { useEffect, useContext } from 'react';
import { useUsers } from '../hooks/useUsers';
import { useSelection } from '../hooks/useSelection';
import UserToolbar from '../components/users/UserToolbar';
import UserTable from '../components/users/UserTable';
import Navbar from '../components/common/Navbar';
import { AuthContext } from '../context/AuthContext';

const UsersPage = () => {
  const { user } = useContext(AuthContext);
  const { users, loading, fetchUsers, blockUsers, unblockUsers, deleteUsers, deleteUnverified } = useUsers();
  const selection = useSelection(users);

  useEffect(() => {
    fetchUsers();
  }, [fetchUsers]);

  const handleBlock = async () => {
    const success = await blockUsers(selection.selectedIds);
    if (success) selection.clear();
  };

  const handleUnblock = async () => {
    const success = await unblockUsers(selection.selectedIds);
    if (success) selection.clear();
  };

  const handleDelete = async () => {
    if (!window.confirm(`Are you sure you want to delete ${selection.selectedIds.length} user(s)?`)) return;
    const success = await deleteUsers(selection.selectedIds);
    if (success) selection.clear();
  };

  const handleDeleteUnverified = async () => {
    if (!window.confirm(`Delete selected unverified users?`)) return;
    const success = await deleteUnverified(selection.selectedIds);
    if (success) selection.clear();
  };

  return (
    <div className="bg-light min-vh-100">
      <Navbar />
      <div className="container pb-5">
        <div className="row mb-4">
          <div className="col">
            <h2 className="fw-bold text-dark">Users Management</h2>
            <p className="text-muted">Manage your application users from this panel.</p>
          </div>
        </div>

        {loading && users.length === 0 ? (
          <div className="d-flex justify-content-center p-5">
            <div className="spinner-border text-primary" role="status">
              <span className="visually-hidden">Loading...</span>
            </div>
          </div>
        ) : (
          <div className="shadow-sm rounded">
            <UserToolbar 
              selectedCount={selection.selectedIds.length}
              onBlock={handleBlock}
              onUnblock={handleUnblock}
              onDelete={handleDelete}
              onDeleteUnverified={handleDeleteUnverified}
            />
            <UserTable 
              users={users}
              selectedIds={selection.selectedIds}
              onToggleId={selection.toggle}
              onToggleAll={selection.toggleAll}
              isAllSelected={selection.isAllSelected}
              isSomeSelected={selection.isSomeSelected}
            />
          </div>
        )}
      </div>
    </div>
  );
};

export default UsersPage;
