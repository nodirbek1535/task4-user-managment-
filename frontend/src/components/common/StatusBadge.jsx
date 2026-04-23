import React from 'react';

const StatusBadge = ({ status }) => {
  let badgeClass = 'bg-secondary';
  let text = 'Unknown';

  if (status === 0 || status === 'Unverified') {
    badgeClass = 'bg-warning text-dark';
    text = 'Unverified';
  } else if (status === 1 || status === 'Active') {
    badgeClass = 'bg-success';
    text = 'Active';
  } else if (status === 2 || status === 'Blocked') {
    badgeClass = 'bg-danger';
    text = 'Blocked';
  }

  return <span className={`badge ${badgeClass}`}>{text}</span>;
};

export default StatusBadge;
