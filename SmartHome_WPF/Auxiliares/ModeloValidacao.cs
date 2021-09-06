using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome_WPF.Auxiliares
{
    public abstract class ModeloValidacao : INotifyDataErrorInfo, INotifyPropertyChanged
    {
        public ModeloValidacao()
        {
            Erros = new Dictionary<string, List<string>>();
        }

        #region ErrorsChanged Membros
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        protected void fireErrorsChanged(String Propriedade)
        {
            if (ErrorsChanged != null)
                ErrorsChanged(this, new DataErrorsChangedEventArgs(Propriedade));
        }
        #endregion

        #region PropertyChanged Membros
        public event PropertyChangedEventHandler PropertyChanged;
        public void firePropertyChanged(String Propriedade)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(Propriedade));
        }
        #endregion

        #region INotifyDataErrorInfo Membros

        private Dictionary<string, List<string>> Erros;
        public void AdicionarErro(String Propriedade, String Erro)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(Propriedade) == false || string.IsNullOrWhiteSpace(Erro) == false)
                {
                    Erros[Propriedade] = new List<string>() { Erro };
                    fireErrorsChanged(Propriedade);
                }
            }
            catch
            {
                return;
            }
        }

        protected abstract void Validar(String Propriedade);

        public void ExecutarValidacoes(String[] Vetor)
        {
            foreach (String Propriedade in Vetor)
                Validar(Propriedade);
        }

        public void RemoverErros(String Propriedade)
        {
            if (Erros.ContainsKey(Propriedade))
                Erros.Remove(Propriedade);
            fireErrorsChanged(Propriedade);
        }

        public void RemoverErros(String[] Vetor)
        {
            foreach (String Propriedade in Vetor)
            {
                if (Erros.ContainsKey(Propriedade))
                    Erros.Remove(Propriedade);
                fireErrorsChanged(Propriedade);
            }
        }

        public System.Collections.IEnumerable GetErrors(string propertyName)
        {
            try
            {
                List<string> errorsForName;
                Erros.TryGetValue(propertyName, out errorsForName);
                return errorsForName;
            }
            catch
            {
                return null;
            }
        }

        public bool HasErrors
        {
            get { return Erros.Count > 0; }
        }
            
        #endregion
    }
}
