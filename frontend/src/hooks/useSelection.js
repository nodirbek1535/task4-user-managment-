import { useState, useCallback } from 'react';

export const useSelection = (items = []) => {
  const [selectedIds, setSelectedIds] = useState([]);

  const toggle = useCallback((id) => {
    setSelectedIds((prev) => 
      prev.includes(id) 
        ? prev.filter((item) => item !== id) 
        : [...prev, id]
    );
  }, []);

  const toggleAll = useCallback((allIds) => {
    setSelectedIds((prev) => 
      prev.length === allIds.length ? [] : [...allIds]
    );
  }, []);

  const clear = useCallback(() => {
    setSelectedIds([]);
  }, []);

  const isSelected = useCallback((id) => selectedIds.includes(id), [selectedIds]);

  const isAllSelected = items.length > 0 && selectedIds.length === items.length;
  const isSomeSelected = selectedIds.length > 0 && selectedIds.length < items.length;

  return {
    selectedIds,
    toggle,
    toggleAll,
    clear,
    isSelected,
    isAllSelected,
    isSomeSelected
  };
};
