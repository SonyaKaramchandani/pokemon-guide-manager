/* eslint-disable @typescript-eslint/no-unused-expressions */
import { renderHook, act } from '@testing-library/react-hooks';
import { useDebouncedState } from './useDebouncedState';
import { expect } from 'chai';

const DebounceTestDelay = 20;
const StartValue = '';

describe('useDebouncedState', () => {
  test('start with empty string, type 3 chars, check debounce before/after', async () => {
    const { result, waitForNextUpdate } = renderHook(() =>
      useDebouncedState(StartValue, DebounceTestDelay)
    );

    act(() => {
      const [
        searchText,
        searchTextDebounced,
        setSearchText,
        setSearchTextForceNoProxy
      ] = result.current;
      setSearchText('c');
      setSearchText('ca');
      setSearchText('can');
    });

    expect(result.current[0]).to.equal('can');
    expect(result.current[1]).to.equal('');

    await waitForNextUpdate();

    expect(result.current[0]).to.equal('can');
    expect(result.current[1]).to.equal('can');
  });

  test('start with empty string, type 3 chars, then null, check debounce before/after', async () => {
    const { result, waitForNextUpdate } = renderHook(() =>
      useDebouncedState(StartValue, DebounceTestDelay)
    );

    act(() => {
      const [
        searchText,
        searchTextDebounced,
        setSearchText,
        setSearchTextForceNoProxy
      ] = result.current;
      setSearchText('c');
      setSearchText('ca');
      setSearchText('can');
      setSearchText(null);
    });

    expect(result.current[0]).to.be.null;
    expect(result.current[1]).to.equal('');

    await waitForNextUpdate();

    expect(result.current[0]).to.be.null;
    expect(result.current[1]).to.be.null;
  });

  test('start with empty string, type 3 chars, wait, type 3 more chars, check before/after', async () => {
    const { result, waitForNextUpdate } = renderHook(() =>
      useDebouncedState(StartValue, DebounceTestDelay)
    );

    act(() => {
      const [
        searchText,
        searchTextDebounced,
        setSearchText,
        setSearchTextForceNoProxy
      ] = result.current;
      setSearchText('c');
      setSearchText('ca');
      setSearchText('can');
    });

    expect(result.current[0]).to.equal('can');
    expect(result.current[1]).to.equal('');

    await waitForNextUpdate();

    expect(result.current[0]).to.equal('can');
    expect(result.current[1]).to.equal('can');

    act(() => {
      const [
        searchText,
        searchTextDebounced,
        setSearchText,
        setSearchTextForceNoProxy
      ] = result.current;
      setSearchText('cana');
      setSearchText('canad');
      setSearchText('canada');
    });

    expect(result.current[0]).to.equal('canada');
    expect(result.current[1]).to.equal('can');

    await waitForNextUpdate();

    expect(result.current[0]).to.equal('canada');
    expect(result.current[1]).to.equal('canada');
  });

  test('setSearchTextForceNoProxy', async () => {
    const { result, waitForNextUpdate } = renderHook(() =>
      useDebouncedState(StartValue, DebounceTestDelay)
    );

    act(() => {
      const [
        searchText,
        searchTextDebounced,
        setSearchText,
        setSearchTextForceNoProxy
      ] = result.current;
      setSearchText('c');
      setSearchText('ca');
      setSearchText('can');
      setSearchTextForceNoProxy('canada');
    });

    expect(result.current[0]).to.equal('canada');
    expect(result.current[1]).to.equal('canada');
  });
});
