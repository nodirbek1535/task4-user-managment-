import React, { useEffect, useRef } from 'react';

const SelectAllCheckbox = ({ isSomeSelected, isAllSelected, onChange }) => {
  const checkboxRef = useRef(null);

  useEffect(() => {
    if (checkboxRef.current) {
      checkboxRef.current.indeterminate = isSomeSelected && !isAllSelected;
    }
  }, [isSomeSelected, isAllSelected]);

  return (
    <input
      type="checkbox"
      className="form-check-input"
      ref={checkboxRef}
      checked={isAllSelected}
      onChange={onChange}
      aria-label="Select all rows"
    />
  );
};

export default SelectAllCheckbox;
