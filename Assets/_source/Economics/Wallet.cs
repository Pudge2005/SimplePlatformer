using System;

namespace Game.Economics
{
    public class Wallet : IWallet
    {
        private readonly int _precision;
        private readonly float _precisionCoefficient;
        private long _data;


        public Wallet(int pointOffset)
        {
            _precision = pointOffset;
            _precisionCoefficient = Fast10Pow(pointOffset);
        }

        public Wallet(int pointOffset, float balance)
        {
            _precision = pointOffset;
            _precisionCoefficient = Fast10Pow(pointOffset);
            SetBalance(balance);
        }


        public event System.Action<IWallet, float> OnBalanceChanged;


        public int Precision => _precision;
        public float Balance => GetBalance();


        public float GetBalance()
        {
            return DataToDec(_data);
        }


        public void SetBalance(float value)
        {
            _data = DecToData(value);
            OnBalanceChanged?.Invoke(this, Balance);
        }


        public void ChangeBalance(float delta)
        {
            _data += DecToData(delta);
            OnBalanceChanged?.Invoke(this, Balance);
        }


        private long DecToData(float dec)
        {
            //var x = dec * _precisionCoefficient; // для отладки по точкам останова
            //var y = Math.Round(x, _precision);   // (1 копейка (0.01) * 100 не равнялась 1)
            //return (long)y;

            return (long)Math.Round(dec * _precisionCoefficient, _precision);
        }

        private float DataToDec(long data)
        {
            return data / _precisionCoefficient;
        }


        private static int Fast10Pow(int zeroesCount)
        {
            int v = 1;

            for (int i = 0; i < zeroesCount; i++)
            {
                v *= 10;
            }

            return v;
        }
    }
}
